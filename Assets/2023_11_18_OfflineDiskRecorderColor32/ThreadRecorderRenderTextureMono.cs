using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

public class ThreadRecorderRenderTextureMono : MonoBehaviour
{

    public Animator m_animator;

    public class ThreadKeepAlive {
        public bool m_mustDie = false;
        public DateTime m_safetyTime;
    }
    public ThreadKeepAlive m_keepAlive = new ThreadKeepAlive();
    public string m_persistancePath;
    public class TextureWithRelativeTimeId
    {
        public int m_relativeTimeInMilliseconds;
        public int m_width = 2048;
        public int m_height = 2048;
        public Color32[] m_color;

        public TextureWithRelativeTimeId(int relativeTimeInMilliseconds, int width, int height, Color32[] color)
        {
            m_relativeTimeInMilliseconds = relativeTimeInMilliseconds;
            m_width = width;
            m_height = height;
            m_color = color;
        }
    }
    public Queue<TextureWithRelativeTimeId> m_stack = new Queue<TextureWithRelativeTimeId>();


    private void OnDestroy()
    {
        m_keepAlive.m_mustDie = true;
    }
    private void Update()
    {
        m_keepAlive.m_safetyTime = DateTime.Now;
    }
    public void Add(Texture2D target)
    {
        Add(target.GetPixels32(), target.width,target.height);
    }
    public float m_percentOfAnimation;
    public float m_secondsOfAnimation;
    public enum TimeType { AnimationTime, GameRelativeTime}
    public TimeType m_timeType;
    public void Add(Color32[] target, int width, int height)
    {
        float t = 0;
        if (m_animator) { 
            float currentTime = m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            float clipDuration = m_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            float currentTimeInSeconds = currentTime * clipDuration;
            m_percentOfAnimation = currentTime;
            m_secondsOfAnimation = currentTimeInSeconds;
            t = m_timeType == TimeType.AnimationTime ? currentTimeInSeconds : Time.time ;
        }
        m_stack.Enqueue( new TextureWithRelativeTimeId ((int)(t * 1000f) , width, height, target) );
    }

    public int m_threadToCreate=3;
    private void Awake()
    {
        m_persistancePath = Application.persistentDataPath;
        Debug.Log(Application.persistentDataPath);
        m_keepAlive.m_safetyTime = DateTime.Now;
        Thread t = null;

        for (int i = 0; i < m_threadToCreate; i++)
        {
            t = new Thread(LoopSaveColor32ToPNG);
            t.Priority = System.Threading.ThreadPriority.Highest;
            t.Start();
        }
        
        InvokeRepeating("FlushGC", 0, 5);
    }
    public void FlushGC() {

        Resources.UnloadUnusedAssets();
        GC.Collect();
    }

    public ulong m_theadDebug = 0;
    public ulong m_theadImage = 0;
    public int m_inWaiting = 0;
    static object queueLock = new object();


    public byte[] ConvertToBytes(Color32[] colors) { return MemoryMarshal.Cast<Color32, byte>(colors).ToArray(); }
    public Color32[] ConvertToColor32(byte[] value) { return MemoryMarshal.Cast<byte,Color32 >(value).ToArray(); }

    private void LoopSaveColor32ToPNG()
    {
        while (true) {
            m_theadDebug++;
            int frameToDoo = 1;
            if (m_inWaiting > 50) 
                frameToDoo = 3;
            if (m_inWaiting > 60) 
                frameToDoo = 10;
            if (m_inWaiting > 200) 
                frameToDoo = 20;

            for (int i = 0; i < frameToDoo; i++)
            {
                if (m_stack.Count > 0)
                {
                    m_theadImage++;
                    TextureWithRelativeTimeId item=null;
                    lock (queueLock)
                    {
                        if (m_stack.Count > 0)
                        {
                            item = m_stack.Dequeue();
                        }
                    }
                    if (item != null) { 
                        byte[] bytes = ConvertToBytes(item.m_color);
                        string fileName = item.m_width + "x" + item.m_height + "_" + System.DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + item.m_relativeTimeInMilliseconds + ".color32";
                        string subfolderPath = Path.Combine(m_persistancePath, "Screenshots");
                        string filePath = Path.Combine(subfolderPath, fileName);

                        if (!Directory.Exists(subfolderPath))
                        {
                            Directory.CreateDirectory(subfolderPath);
                        }

                        System.IO.File.WriteAllBytes(filePath, bytes);
                    }
                }
                m_inWaiting = m_stack.Count;
            }
            //GC.Collect();
            Thread.Sleep(1);
            if (m_keepAlive.m_mustDie)
                return;
            if ((DateTime.Now- m_keepAlive.m_safetyTime).TotalSeconds>20)
                return;
        }
    }
}

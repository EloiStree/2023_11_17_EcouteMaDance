using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVToPoseProbabilityMono : MonoBehaviour
{

    public string m_pathDirectory;

    [System.Serializable]
    public class EventFound {

        public int m_milliseconds;
        public float m_probability;
        public string m_poseName;

        public EventFound(int milliseconds, float probability, string poseName)
        {
            m_milliseconds = milliseconds;
            m_probability = probability;
            m_poseName = poseName;
        }
    }
    public List<EventFound> eventFound = new List<EventFound>();

    public float m_precentMintoBeAccepted = 0.7f;
    public bool m_allowEqualOne = true;
    [ContextMenu("Import")]
    public void Import() {
        eventFound.Clear();
        string[] paths = Directory.GetFiles(m_pathDirectory, "*.csv", SearchOption.TopDirectoryOnly);
        foreach (var path in paths)
        {
            string p = path.Replace(".csv","");
            int indexStart= p.LastIndexOf('_');
            if (indexStart <0) continue;
            p =  p.Substring(indexStart + 1);
            indexStart= p.IndexOf(".");
            p = p.Substring(0, indexStart);

            Debug.Log("--"+p);
            if (int.TryParse(p, out int ms)) {
              
                string text = File.ReadAllText(path);
                string[] lines = text.Split('\n');
                foreach (var line in lines)
                {
                    string[] cells = line.Split(',');
                    if (cells.Length == 2)
                    {
                        if (float.TryParse(cells[0], out float probability))
                        {
                            if (!m_allowEqualOne && ( probability > m_precentMintoBeAccepted && probability < 1.0f))
                            {
                                eventFound.Add(new EventFound(ms, probability, cells[1]));
                            }
                            else if (m_allowEqualOne && (probability > m_precentMintoBeAccepted && probability <= 1.0f))
                            {
                                eventFound.Add(new EventFound(ms, probability, cells[1]));
                            }
                        }
                    }
                }
            }
            

        }


    }

    //public class ImportedPoseProbability { 
    //    public 
    //}
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}

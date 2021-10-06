using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    public GameObject FirstPlatform;
    public GameObject NewPlatform;
    public GameObject NewTarget;
    public bool playing = true;
    public float StartSpeed = 1;
    public float SpeedAcceleration = 1;
    public int MaxPlatforms = 3;
    public int MaxTargets = 2;
    private List<GameObject> Platforms = new List<GameObject>();
    private List<GameObject> ToDestory = new List<GameObject>();
    private List<GameObject> Targets = new List<GameObject>();
    void Start()
    {
        Platforms.Add(FirstPlatform);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Platforms.Count < MaxPlatforms && Targets.Count == 0) {
            Vector3 randompos = new Vector3(Random.Range(20f,30f),Random.Range(-2f,2f),0f);
            Platforms.Add(Instantiate(NewPlatform, Platforms[Platforms.Count-1].transform.position + randompos, Platforms[Platforms.Count-1].transform.rotation));
            
            
            for (int i = 0; i < MaxTargets; i++) {

                Vector3 randomposTarget = new Vector3(20f,Random.Range(20f,30f),Random.Range(-30f,30f));
                randomposTarget += Platforms[Platforms.Count-1].transform.position;
                Vector3 towardsPlatform = (Platforms[Platforms.Count-1].transform.position - randomposTarget).normalized;
                Targets.Add(Instantiate(NewTarget, randomposTarget, Quaternion.LookRotation(towardsPlatform) * Quaternion.Euler(90,0,0)));
            }

        }
        if (Platforms.Count > 1) {
            if (Vector3.Distance(Platforms[0].transform.position,Platforms[1].transform.position) > 100f) {
                ToDestory.Add(Platforms[0]);
            }
        }

        foreach(GameObject platform in Platforms) {
            if (platform.GetComponent<Platform>().invisible) {
                ToDestory.Add(platform);
            }
        }
        foreach(GameObject target in Targets) {
            if (target.GetComponent<Target>().dead) {
                ToDestory.Add(target);
            }
        }
        if (ToDestory.Count > 0) {
            if (ToDestory[0].tag == "Platform") {
                Platforms.Remove(ToDestory[0]);
                Destroy(ToDestory[0]);
                ToDestory.Remove(ToDestory[0]);
            } else if (ToDestory[0].tag == "Shootable") {
                Targets.Remove(ToDestory[0]);
                Destroy(ToDestory[0]);
                ToDestory.Remove(ToDestory[0]);
            }
            

        }
    }

}

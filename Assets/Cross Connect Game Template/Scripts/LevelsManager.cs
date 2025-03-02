using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BiffeProd
{
    public class LevelsManager : MonoBehaviour
    {
        [Serializable]
        public struct LevelsData
        {
            public string categorieName;
            public List<string> levels;
            public List<string> txtDescription;
        }
        public List<LevelsData> levelsData;
        public GameObject categoriesPanel;
        public GameObject levelsPanel;
        public GameObject categorieFrame;
        public GameObject levelFrame;
        public Transform categoriesParent;
        public Transform levelsParent;
        public Sprite onLevel;
        public Sprite offLevel;
        public AudioSource sfx;

        private void Start()
        {
            levelsPanel.SetActive(false);
            categoriesPanel.SetActive(true);
            loadCategories();

        }
        private void loadCategories()
        {
            foreach (LevelsData cat in levelsData)
            {
                GameObject obj = Instantiate(categorieFrame.gameObject, categoriesParent);
                obj.GetComponentInChildren<Text>().text = cat.categorieName;
                obj.GetComponent<Button>().onClick.AddListener(delegate
                {
                    sfx.Play();
                    levelsPanel.SetActive(true);
                    categoriesPanel.SetActive(false);

                    int i = 0;
                    foreach (string lvl in cat.levels)
                    {
                        i++;
                        //instatiate
                        GameObject obj = Instantiate(levelFrame, levelsParent);
                        obj.GetComponentInChildren<Text>().text = i.ToString();

                        //check if this level is passed
                        if (PlayerPrefs.HasKey(lvl))
                        {
                            obj.GetComponent<Image>().sprite = onLevel;
                            //level passed and it's on
                        }
                        else
                        {
                            obj.GetComponent<Image>().sprite = offLevel;
                        }
                        PlayerPrefs.SetString(lvl + "Desc", cat.txtDescription[i - 1]);

                        obj.GetComponent<Button>().onClick.AddListener(delegate
                        {
                            sfx.Play();

                            //load scene and send the word to the levelcreator
                            PlayerPrefs.SetString("lvlWord", lvl);

                            SceneManager.LoadScene("Game");
                        });
                    }
                });
            }
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject grass;
    [SerializeField] GameObject road;
    [SerializeField] Player player;
    [SerializeField] int extent = 7;
    [SerializeField] int frontDistance = 10;
    [SerializeField] int minZPos = -5;
    [SerializeField] int maxSameTerrainRepeat = 3;
    [SerializeField] GameObject gameOverPanel;

    public bool IsDie { get => this.enabled == false; }


    // int maxZPos;

    Dictionary<int, TerrainBlock> map = new Dictionary<int, TerrainBlock>(50);
    TMP_Text gameOverText;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        gameOverText = gameOverPanel.GetComponentInChildren<TMP_Text>();

        //belakang
        for (int z = minZPos; z <= 0; z++)
        {
            CreateTerrain(grass, z);
        }

        // depan
        for (int z = 1; z < frontDistance; z++)
        {
            var prefab = GetNextRandomTerrainPrefab(z);
            // instantiate bloknya
            CreateTerrain(prefab, z);
        }
        // Debug.Log(Tree.AllPositions.Count);
        // foreach (var treePos in Tree.AllPositions)
        // {
        //     Debug.Log(treePos);
        // }

        player.SetUp(minZPos, extent);
    }

    private void Update()
    {   
        //check player masih hidup ga?
        if(player.IsDie && gameOverPanel.activeInHierarchy == false)
            StartCoroutine(ShowGameOverPanel());
    }

    IEnumerator ShowGameOverPanel()
    {
        yield return new WaitForSeconds(3);
        gameOverText.text = "YOUR SCORE: "+ player.MaxTravel; 
        gameOverPanel.SetActive(true);
    }


    // Debug.Log(Tree.AllPositions)

    private void CreateTerrain(GameObject prefab, int zPos)
    {
        var go = Instantiate(prefab, new Vector3(0, 0, zPos), Quaternion.identity);
        var tb = go.GetComponent<TerrainBlock>();
        tb.Build(extent);
        map.Add(zPos, tb);
        // Debug.Log(map[zPos] is Road);
    }

    private GameObject GetNextRandomTerrainPrefab(int nextPos)
    {
        bool isUniform = true;
        var tbRef = map[nextPos - 1];
        for (int distance = 2; distance <= maxSameTerrainRepeat; distance++)
        {
            if (map[nextPos - distance].GetType() != tbRef.GetType())
            {
                isUniform = false;
                break;
            }
        }

        if (isUniform)
        {
            if (tbRef is Grass)
                return road;
            else
                return grass;
        }

        // penentuan terrain blok dengan probabilitas 50%
        return Random.value > 0.5 ? road : grass;
    }


}
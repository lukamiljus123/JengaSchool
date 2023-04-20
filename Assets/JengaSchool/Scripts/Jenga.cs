using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Jenga : MonoBehaviour
{
    [SerializeField] private string _grade;
    [SerializeField] private KeyCode _keyCode;
    
    [SerializeField] private TMP_Text _gradeText;
    private Blocks _blocks;
    private readonly List<GameObject> blockGameObjects = new();

    public string Grade => _grade;
    public KeyCode KeyCode => _keyCode;
    
    private void Start()
    {
        _blocks.blocks = new List<Block>();
        ReferenceManager.Instance.ApiManager.OnLoaded.AddListener(Init);
        UpdateGradeText();
    }

    private void UpdateGradeText()
    {
        _gradeText.text = _grade;
    }

    private void Init(Blocks blocks)
    {
        foreach (Block block in blocks.blocks)
        {
            if (block.grade == _grade)
            {
                _blocks.blocks.Add(block);
            }
        }
        
        // Order the blocks in the stack starting from the bottom up
        // by domain name ascending
        // then by cluster name ascending
        // then by standard ID ascending
        _blocks.blocks = _blocks.blocks.OrderBy(x => x.domain)
            .ThenBy(x => x.cluster)
            .ThenBy(x => x.standardid)
            .ToList();

        int i = 0;
        float yOffset = 0.5f;
        float xSpacing = 2.75f;
        float ySpacing = 1.5f;
        foreach (Block block in _blocks.blocks)
        {
            Vector3 position;
            Quaternion rotation;
            if (i % 6 > 2)
            {
                position = new Vector3(transform.position.x + xSpacing, i / 3 * ySpacing + yOffset, transform.position.z + (i % 3 - 1) * xSpacing);
                rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            else
            {
                position = new Vector3(i % 3 * xSpacing, i / 3 * ySpacing + yOffset, transform.position.z);
                rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }

            GameObject newBlock = Instantiate(ReferenceManager.Instance.BlockPrefabs[(int)block.mastery], position,
                rotation, transform);
            blockGameObjects.Add(newBlock);
            newBlock.GetComponent<BlockView>().Block = block;
            i++;
        }
    }

    public void TestMyStack()
    {
        foreach (GameObject block in blockGameObjects.ToList())
        {
            if (block.name.ToLower().Contains("glass"))
            {
                blockGameObjects.Remove(block);
                Destroy(block);
            }
        }
        foreach (Block block in _blocks.blocks.ToList())
        {
            if (block.mastery == Mastery.Glass)
            {
                _blocks.blocks.Remove(block);
            }
        }
    }
}
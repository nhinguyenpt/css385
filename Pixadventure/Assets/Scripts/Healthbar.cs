using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Player playerHealth;
    [SerializeField] private Image maxHealthImg;
    [SerializeField] private Image currentHealthImg;


    // Start is called before the first frame update
    void Start()
    {
        maxHealthImg.fillAmount = playerHealth.CurrentHealth / 10;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealthImg.fillAmount = playerHealth.CurrentHealth / 10;
    }
}

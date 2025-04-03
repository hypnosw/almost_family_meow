using TMPro;
using UnityEngine;

public class HideoutBehavior : MonoBehaviour
{
    public float detectionRange = 2f;
    public TMP_Text hideoutText;

    private bool isNear = false;
    public static bool isHidden { get; private set; }
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // hideoutText.enabled = false;
    }

    void Update()
    {


        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange);
        isNear = false;

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                isNear = true;
                break;
            }
        }

        Debug.Log($"isNear: {isNear}, isHidden: {isHidden}");

        if (!isHidden && isNear)
        {
            hideoutText.enabled = true;
            hideoutText.text = "Press F to hide";

            if (Input.GetKeyDown(KeyCode.F))
            {
                HidePlayer();
            }
        }
        else if (!isHidden)
        {
            hideoutText.enabled = false;
        }

        if (isHidden && Input.GetKeyDown(KeyCode.E))
        {
            UnhidePlayer();
        }
    }

    void HidePlayer()
    {
        // player.transform.position = transform.position;

        player.SetActive(false);
        isHidden = true;

        hideoutText.enabled = true;
        hideoutText.text = "Press E to leave";
    }

    void UnhidePlayer()
    {
        player.SetActive(true);
        isHidden = false;
        hideoutText.enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

}

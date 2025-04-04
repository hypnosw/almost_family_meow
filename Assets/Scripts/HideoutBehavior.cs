using TMPro;
using UnityEngine;

public class HideoutBehavior : MonoBehaviour
{
    public float detectionRange = 2f;
    public TMP_Text hideoutText;

    private bool isNear = false;
    private bool wasNear = false;
    public static bool isHidden { get; private set; }
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hideoutText.enabled = false;
    }

    void Update()
    {
        wasNear = isNear;
        isNear = false;

        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                isNear = true;
                break;
            }
        }

        // If player is hidden, always show "Press E to leave"
        if (isHidden)
        {
            hideoutText.text = "Press E to leave";
            hideoutText.enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                UnhidePlayer();
            }
        }
        // If player is near and not hidden, show "Press F to hide"
        else if (isNear)
        {
            hideoutText.text = "Press F to hide";
            hideoutText.enabled = true;

            if (Input.GetKeyDown(KeyCode.F))
            {
                HidePlayer();
            }
        }
        // If player is not near and not hidden, hide the text
        else if (wasNear && !isNear)
        {
            hideoutText.enabled = false;
        }
    }

    void HidePlayer()
    {
        player.SetActive(false);
        isHidden = true;
    }

    void UnhidePlayer()
    {
        player.SetActive(true);
        isHidden = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

}

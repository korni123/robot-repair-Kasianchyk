using UnityEngine;
public class HealingZone : MonoBehaviour
{
    public int amount = 1;
    public float interval = 1.0f;
    float timer;
    void OnTriggerStay2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller != null && controller.health < controller.maxHealth)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                controller.ChangeHealth(amount);
                timer = interval;
            }
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
public abstract class Entity : MonoBehaviour
{
    public Transform face;
    private NavMeshAgent agent;
    public string uniqueName;
    public string faction;
    public string displayName;
    public Sprite sprite;
    public static Action<string> OnUniqueNPCDeath, OnFactionMemberDeath, OnEntityHeal;

    public bool interactable = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public abstract bool Interact(PlayerData data);

    public void Die()
    {
        if (uniqueName != null && uniqueName.Length > 0)
        {
            OnUniqueNPCDeath?.Invoke(uniqueName);
        }
        if (faction != null && faction.Length > 0)
        {
            OnFactionMemberDeath?.Invoke(faction);
        }
    }

    public abstract void Heal();

    public abstract void TurnHostile(PlayerData data);

    public void Despawn()
    {
        Destroy(gameObject);
    }

}

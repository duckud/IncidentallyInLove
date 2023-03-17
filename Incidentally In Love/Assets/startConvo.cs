using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor; 

public class startConvo : MonoBehaviour
{
    //der ganze code ist von https://www.youtube.com/watch?v=HbrNrvX2Ft4&list=PLfRF6lnXtGqjrhzyQhidqMD-shMHGReXi&index=1 

    public NPCConversation myConversation;

    void Start()
    {
        ConversationManager.Instance.StartConversation(myConversation);
    }
}

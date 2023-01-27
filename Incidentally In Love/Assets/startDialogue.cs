using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

//source: https://www.youtube.com/watch?v=HbrNrvX2Ft4&list=PLfRF6lnXtGqjrhzyQhidqMD-shMHGReXi&index=2
public class startDialogue : MonoBehaviour
{
  // NPCConversation Variable (assigned in Inspector)
 public NPCConversation Conversation;

 private void OnMouseOver()
 {
 if (Input.GetMouseButtonDown(0))
 {
 ConversationManager.Instance.StartConversation(Conversation);
 }
 }
}

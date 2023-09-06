using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HeneGames.DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        private int currentSentence;
        private float coolDownTimer;
        private bool dialogueIsOn;
        private DialogueTrigger dialogueTrigger;

        //------Extended Variables
        private string[] currentSections;
        private int currentSectionID=0;
        private int maxSectionsInSentence=0;
        private bool isSentenceFinished=false;
        //------------------------

        public enum TriggerState
        {
            Collision,
            Input
        }

        [Header("References")]
        [SerializeField] private AudioSource audioSource;

        [Header("Events")]
        public UnityEvent startDialogueEvent;
        public UnityEvent nextSentenceDialogueEvent;
        public UnityEvent endDialogueEvent;

        [Header("Dialogue")]
        [SerializeField] private TriggerState triggerState;
        [SerializeField] private List<NPC_Centence> sentences = new List<NPC_Centence>();

        private void Update()
        {
            //Timer
            if(coolDownTimer > 0f)
            {
                coolDownTimer -= Time.deltaTime;
            }

            //Start dialogue by input
            if (Input.GetKeyDown(DialogueUI.instance.actionInput) && dialogueTrigger != null && !dialogueIsOn)
            {
                //Trigger event inside DialogueTrigger component
                if (dialogueTrigger != null)
                {
                    dialogueTrigger.startDialogueEvent.Invoke();
                }

                startDialogueEvent.Invoke();

                //If component found start dialogue
                DialogueUI.instance.StartDialogue(this);

                //Hide interaction UI
                DialogueUI.instance.ShowInteractionUI(false);

                dialogueIsOn = true;
            }
        }

        //Start dialogue by trigger
        private void OnTriggerEnter(Collider other)
        {
            if (triggerState == TriggerState.Collision)
            {
                //Try to find the "DialogueTrigger" component in the crashing collider
                if (other.gameObject.TryGetComponent<DialogueTrigger>(out DialogueTrigger _trigger))
                {
                    //Trigger event inside DialogueTrigger component and store refenrece
                    dialogueTrigger = _trigger;
                    dialogueTrigger.startDialogueEvent.Invoke();

                    startDialogueEvent.Invoke();

                    //If component found start dialogue
                    DialogueUI.instance.StartDialogue(this);

                    dialogueIsOn = true;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (triggerState == TriggerState.Collision)
            {
                //Try to find the "DialogueTrigger" component in the crashing collider
                if (collision.gameObject.TryGetComponent<DialogueTrigger>(out DialogueTrigger _trigger))
                {
                    //Trigger event inside DialogueTrigger component and store refenrece
                    dialogueTrigger = _trigger;
                    dialogueTrigger.startDialogueEvent.Invoke();

                    startDialogueEvent.Invoke();

                    //If component found start dialogue
                    DialogueUI.instance.StartDialogue(this);
                }
            }
        }

        //Start dialogue by pressing DialogueUI action input
        private void OnTriggerStay(Collider other)
        {
            if (dialogueTrigger != null)
                return;

            if (triggerState == TriggerState.Input && dialogueTrigger == null)
            {
                //Try to find the "DialogueTrigger" component in the crashing collider
                if (other.gameObject.TryGetComponent<DialogueTrigger>(out DialogueTrigger _trigger))
                {
                    //Show interaction UI
                    DialogueUI.instance.ShowInteractionUI(true);

                    //Store refenrece
                    dialogueTrigger = _trigger;
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (dialogueTrigger != null)
                return;

            if (triggerState == TriggerState.Input && dialogueTrigger == null)
            {
                //Try to find the "DialogueTrigger" component in the crashing collider
                if (collision.gameObject.TryGetComponent<DialogueTrigger>(out DialogueTrigger _trigger))
                {
                    //Show interaction UI
                    DialogueUI.instance.ShowInteractionUI(true);

                    //Store refenrece
                    dialogueTrigger = _trigger;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //Try to find the "DialogueTrigger" component from the exiting collider
            if (other.gameObject.TryGetComponent<DialogueTrigger>(out DialogueTrigger _trigger))
            {
                //Hide interaction UI
                DialogueUI.instance.ShowInteractionUI(false);

                //Stop dialogue
                StopDialogue();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            //Try to find the "DialogueTrigger" component from the exiting collider
            if (collision.gameObject.TryGetComponent<DialogueTrigger>(out DialogueTrigger _trigger))
            {
                //Hide interaction UI
                DialogueUI.instance.ShowInteractionUI(false);

                //Stop dialogue
                StopDialogue();
            }
        }

        public void StartDialogue()
        {
            //Cooldown timer
            coolDownTimer = 0.5f;

            //Start event
            if(dialogueTrigger != null)
            {
                dialogueTrigger.startDialogueEvent.Invoke();
            }

            startDialogueEvent.Invoke();

            //Reset sentence index
            //currentSentence = 0; DONE--> this is the behavior we dont want
            ProvideCurrentSentence();

            //Show first sentence in dialogue UI
            ShowCurrentSentence();

            //Play dialogue sound
            PlaySound(sentences[currentSentence].sentenceSound);
        }

        //TODO--> Stop showing text when showing the last section of the current sentence and increase the current sentece ID to start from there next interaction
        public void NextSentence(out bool lastSentence)
        {
            //The next sentence cannot be changed immediately after starting
            if (coolDownTimer > 0f)
            {
                lastSentence = false;
                return;
            }

            //Add one to sentence index
            //currentSentence++;
            currentSectionID++;

            //Next sentence event
            if (dialogueTrigger != null)
            {
                dialogueTrigger.nextSentenceDialogueEvent.Invoke();
            }

            nextSentenceDialogueEvent.Invoke();

            //If last sentence stop dialogue and return
            //if (currentSentence > sentences.Count - 1)
            //{
              //  StopDialogue();

                //lastSentence = true;

                //return;
            //}
            if (currentSectionID > maxSectionsInSentence) //TODO Revisar mas tarde
            {
                StopDialogue();

                lastSentence = true;
                isSentenceFinished = true;

                return;
            }

            //If not last sentence continue...
            lastSentence = false;

            //Play dialogue sound
            PlaySound(sentences[currentSentence].sentenceSound);

            //Show next sentence in dialogue UI
            ShowCurrentSentence();

            //Cooldown timer
            coolDownTimer = 0.5f;
        }

        public void StopDialogue()
        {
            //Stop dialogue event
            if (dialogueTrigger != null)
            {
                dialogueTrigger.endDialogueEvent.Invoke();
            }

            endDialogueEvent.Invoke();

            //Hide dialogue UI
            DialogueUI.instance.ClearText();

            //Stop audiosource so that the speaker's voice does not play in the background
            if(audioSource != null)
            {
                audioSource.Stop();
            }

            //Remove trigger refence
            dialogueIsOn = false;
            dialogueTrigger = null;
        }

        private void PlaySound(AudioClip _audioClip)
        {
            //Play the sound only if it exists
            if (_audioClip == null || audioSource == null)
                return;

            //Stop the audioSource so that the new sentence does not overlap with the old one
            audioSource.Stop();

            //Play sentence sound
            audioSource.PlayOneShot(_audioClip);
        }
        
        //TODO --> Secuentially show all sections of the current sentence instead of all avaliable sentences
        private void ShowCurrentSentence()
        {
            if (sentences[currentSentence].dialogueCharacter != null)
            {
                //Show sentence on the screen
                DialogueUI.instance.ShowSentence(sentences[currentSentence].dialogueCharacter, currentSections[currentSectionID]);//sentences[currentSentence].sentence);

                //Invoke sentence event
                sentences[currentSentence].sentenceEvent.Invoke();
            }
            else
            {
                DialogueCharacter _dialogueCharacter = new DialogueCharacter();
                _dialogueCharacter.characterName = "";
                _dialogueCharacter.characterPhoto = null;

                DialogueUI.instance.ShowSentence(_dialogueCharacter, currentSections[currentSectionID]);//sentences[currentSentence].sentence);
            }
        }


        //-----------Extended Code

        private void ProvideCurrentSentence()
        {
            if(isSentenceFinished)
            {
                if(currentSentence<sentences.Count-1)
                {
                    currentSentence++;
                }

                isSentenceFinished = false;
                currentSectionID = 0;
            }
            currentSections = new string[sentences[currentSentence].sectionsCount];
            DivideSentenceInSections();
        }
        private void DivideSentenceInSections()
        {
            int sectionID = 0;
            string _letters = sentences[currentSentence].sentence;
            foreach (char _letter in _letters)
            {
                if(_letter == '+')
                {
                    sectionID++;
                }
                else
                {
                    currentSections[sectionID] += _letter;
                }
                
            }
            maxSectionsInSentence = sectionID;
        }

    }

   


    [System.Serializable]
    public class NPC_Centence
    {
        [Header("------------------------------------------------------------")]

        public DialogueCharacter dialogueCharacter;

        [TextArea(3, 10)]
        public string sentence;

        public AudioClip sentenceSound;

        public UnityEvent sentenceEvent;

        public int sectionsCount;
    }
}
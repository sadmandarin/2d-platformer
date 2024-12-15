using UnityEngine;
using UnityEngine.InputSystem;

public class NoteItteraction : MonoBehaviour
{
    public Note Note;
    private Inputs _inputs;

    private void Awake()
    {
        _inputs = new Inputs();

        if (Note.IsReaded)
        {
            Destroy(GetComponent<BoxCollider2D>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            Subscription();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            Unsubscription();
        }
    }

    public void Subscription()
    {
        Debug.Log("Podписка");

        _inputs.GamePlay.Interaction.performed += OnTakeNote;

        _inputs.Enable();
    }

    public void Unsubscription()
    {
        Debug.Log("Отписка");

        _inputs.GamePlay.Interaction.performed -= OnTakeNote;

        _inputs.Disable();
    }

    private void OnTakeNote(InputAction.CallbackContext callbackContext)
    {
        Debug.Log(Note.Name);
        Debug.Log(Note.NoteText);

        NotesAndBestiaryManager.Instance.AddToReadNotes(Note);
        GetComponent<BoxCollider2D>().enabled = false;
    }
}

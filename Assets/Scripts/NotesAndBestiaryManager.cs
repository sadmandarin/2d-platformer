using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class NotesAndBestiaryManager : MonoBehaviour
{
    public static NotesAndBestiaryManager Instance;

    private string _linkNotes = "Assets/ScriptableObjects/Notes/NotesTorageSO.asset";
    private string _bestiaryLink = "Assets/ScriptableObjects/Bestiary/BestiaryStorage.asset";

    private NotesStorage _notesStorage;
    private BestiaryStorage _bestiaryStorage;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadNoteStorage();
        LoadBestiaryStorage();
    }

    private void LoadNoteStorage()
    {
        Addressables.LoadAssetAsync<NotesStorage>(_linkNotes).Completed += OnNotesStorageLoaded;
    }

    private void LoadBestiaryStorage()
    {
        Addressables.LoadAssetAsync<BestiaryStorage>(_bestiaryLink).Completed += OnBestiaryStorageLoaded;
    }

    private void OnNotesStorageLoaded(AsyncOperationHandle<NotesStorage> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _notesStorage = handle.Result;
        }
        else
            Debug.Log("NotesStorage not loaded");
    }

    private void OnBestiaryStorageLoaded(AsyncOperationHandle<BestiaryStorage> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _bestiaryStorage = handle.Result;
        }
        else
            Debug.Log("Bestiary not loaded");
    }

    public void AddToReadNotes(Note note)
    {
        if (!note.IsReaded)
        {
            _notesStorage.Notes.Add(note);
            note.IsReaded = true;
        }
    }

    public void AddToReadsBestiary(Bestiary bestiary)
    {
        if (!bestiary.IsReaded)
        {
            _bestiaryStorage.BestiaryList.Add(bestiary);
            bestiary.IsReaded = true;
        }
    }

    public void ShowAllNotes()
    {

    }
}

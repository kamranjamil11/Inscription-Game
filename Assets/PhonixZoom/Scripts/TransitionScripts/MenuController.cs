using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Page InitialPage;
    [SerializeField]
    private GameObject FirstFocusItem;

    private Canvas RootCanvas;
   
    public Stack<Page> PageStack = new Stack<Page>();

    private void Awake()
    {
        RootCanvas = GetComponent<Canvas>();
        DontDestroyOnLoad(this);
       // print("ST"+gameObject.name);
    }

    public void SetInitialPage(Page page)
    {
        PageStack.Clear();
        InitialPage = page;
        FirstFocusItem = page.gameObject;
        if (FirstFocusItem != null)
        {
            EventSystem.current.SetSelectedGameObject(FirstFocusItem);
        }

        if (InitialPage != null)
        {
            PushPage(InitialPage);
        }
    }
    private void Start()
    {
        // if (FirstFocusItem != null)
        // {
        //     EventSystem.current.SetSelectedGameObject(FirstFocusItem);
        // }
        //
        // if (InitialPage != null)
        // {
        //     PushPage(InitialPage);
        // }
       
    }
    
    private void OnCancel()
    {
        if (RootCanvas.enabled && RootCanvas.gameObject.activeInHierarchy)
        {
            if (PageStack.Count != 0)
            {
                PopPage();
            }
        }
    }

    public bool IsPageInStack(Page Page)
    {
        return PageStack.Contains(Page);
    }

    public bool IsPageOnTopOfStack(Page Page)
    {
        return PageStack.Count > 0 && Page == PageStack.Peek();
    }

    public void PushPage(Page Page)
    {
        Page.Enter(true);

        if (PageStack.Count > 0)
        {
            Page currentPage = PageStack.Peek();

            if (currentPage.ExitOnNewPagePush)
            {
                currentPage.Exit(false);
            }
        }

        PageStack.Push(Page);
    }

    public void PopPage()
    {
        if (PageStack.Count > 1)
        {
            Page page = PageStack.Pop();
            page.Exit(true);

            Page newCurrentPage = PageStack.Peek();
            if (newCurrentPage.ExitOnNewPagePush)
            {
                newCurrentPage.Enter(false);
            }
            
        }
        else
        {
            Debug.LogWarning("Trying to pop a page but only 1 page remains in the stack!");
        }
    }
    public void PopAllPages()
    {
        for (int i = 1; i < PageStack.Count; i++)
        {
            PopPage();
        }
    }
    
}

using UnityEngine;

public class UserInterfaceController : MonoBehaviour
{
    public void EnableObject(GameObject obj)
    {

        obj.SetActive(true);
    }

    public void DisableObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void QuitGame()
    {
        print("Quiting game..");
        Application.Quit();
    }
    
    //function for buttons

    public void StartBtn()
    {
        
    }
    
    public void SettingBtn()
    {
        
    }
    
    public void QuitBtn()
    {
        print("Quiting game..");
        Application.Quit();
    }

    public void SaveBtn()
    {
        
    }
}

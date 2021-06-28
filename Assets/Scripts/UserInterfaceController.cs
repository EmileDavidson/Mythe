using UnityEngine;

public class UserInterfaceController : MonoBehaviour
{
    /// <summary>
    /// enables the object.
    /// </summary>
    /// <param name="obj">object to enable</param>
    public void EnableObject(GameObject obj)
    {
        obj.SetActive(true);
    }

    /// <summary>
    /// disables the object.
    /// </summary>
    /// <param name="obj">object to dsiable</param>
    public void DisableObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    /// <summary>
    /// closes the game.
    /// </summary>
    public void QuitGame()
    {
        print("Quiting game..");
        Application.Quit();
    }
}

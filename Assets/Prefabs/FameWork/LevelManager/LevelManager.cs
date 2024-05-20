using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(menuName ="LevelManager")]
public class LevelManager : ScriptableObject 
{
[SerializeField]int MainMenuBuildIndex=0;
[SerializeField]int FirstLevelBuildIndex=1;

public void GoToMainMenu(){
    LoadSenceByIndex(MainMenuBuildIndex);
}
public void LoadFirstLevel(){
    LoadSenceByIndex(FirstLevelBuildIndex);
}
public void RestartCurrentLevel(){
    LoadSenceByIndex(SceneManager.GetActiveScene().buildIndex);
}
private void LoadSenceByIndex(int index){
    SceneManager.LoadScene(index);
    GamePlayStatic.SetGamePause(false);
}
    
}


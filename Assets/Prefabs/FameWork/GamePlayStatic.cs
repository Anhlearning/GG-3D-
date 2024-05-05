using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  static class GamePlayStatic 
{
  public static void SetGamePause(bool paused)
  {
    Time.timeScale=paused ? 0 :1;
  }
  
}

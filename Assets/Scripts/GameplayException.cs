using UnityEngine;

public class GameplayException : System.Exception {
    public GameplayException(): base() { }
    public GameplayException(string message) : base(message) { }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnGamePauseEvent : UnityEvent<bool> { };

public class OnPlayerWinEvent : UnityEvent { };
public class OnDeathEvent : UnityEvent { };
public class OnUpdateUIXP : UnityEvent<int, float, float> { };
public class OnEnemySpawn : UnityEvent { };
public class OnExpDrop : UnityEvent<Vector3> { };
public class OnLevelUpEvent : UnityEvent<bool> { };
public class InteractEvent : UnityEvent<GameObject> { };
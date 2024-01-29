﻿public enum ListenType
{
    // chứa tên của tất cã các loại event trong game
    ANY = 0,
    LEFT_MOUSE_CLICK,
    RIGHT_MOUSE_CLICK,
    UPDATE_PLAYER_INFO,
    RELOAD_ANIMATION_EVENT,

}

public enum UIType
{
    UNKNOWN,
    SCREEN,
    POPUP,
    NOTIFY,
    OVERLAP,

}

public enum WeaponSlot
{
    Primary = 0,
    Secondary = 1,
}

public enum AIStateID
{
    ChasePlayer,
    Death,
}
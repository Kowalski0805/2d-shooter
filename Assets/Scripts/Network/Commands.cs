﻿public enum Commands : byte {
    
    RESERVED = 0b00000000,
    PLAYER_CONNECT = 0b00000001,
    PLAYER_DISCONNECT = 0b00000010,
    ID_RESPONSE = 0b00000010,
    GAME_START = 0b00000011,
    SHOT_FIRED = 0b00000100,
    POSITION_INFO = 0b00000101,
    PLAYER_DAMAGE = 0b00000110,
    PLAYER_DEATH = 0b00000111,
    JOIN_ROOM = 0b00001000,
    JOINED_ROOM = 0b00001001,

}
///SuperSocket_status()
var timeout = current_time - ping[0];
if(timeout > 5000) {
    SuperSocket_disconnect();
}
if(SuperSocket_isConnected() == -1) {
    SuperSocket_reconnect();
    global.login = false;
    room_goto(rm_connect);
}
if(SuperSocket_isConnected() == 1) {
    if(room != rm_login)and(global.login == false) {
        room_goto(rm_login);
    }
}

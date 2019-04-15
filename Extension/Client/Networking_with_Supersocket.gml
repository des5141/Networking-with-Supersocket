#define SuperSocket_init
///SuperSocket_init();
global.message_processing = ds_queue_create();
global.message_buffer = -1;
global.check_bytes_recv = 0;
global.check_bytes_send = 0;
global.reconnect_ip = "";
global.reconnect_port = 9999;
network_set_config(network_config_connect_timeout, 200);

#define SuperSocket_connect
///SuperSocket(ip, port);
global.reconnect_ip = argument0;
global.reconnect_port = argument1;
var ins = instance_create(0, 0, sys_network);
ins.status = 0; // connecting
ins.socket = network_create_socket(network_socket_tcp);
if(network_connect_raw(ins.socket, argument0, argument1) >= 0) {
    ins.status = 1; // connected!
    ping[0] = current_time;
    ping[1] = current_time;
}else {
    instance_destroy(ins);
}

#define SuperSocket_disconnect
///SuperSocket_disconnect();
with(sys_network) {
    instance_destroy();
}

#define SuperSocket_createBuffer
///SuperSocket_createBuffer(Type, SendTo, Space);

if(argument1 == SuperSocket.SendToClient){
    global.buffer_mode = true;
}

var buffer = buffer_create(1024, buffer_grow, 1);
buffer_write(buffer, buffer_u32, 0);
buffer_write(buffer, buffer_u16, argument1); // sendTo
buffer_write(buffer, buffer_s16, argument2); // space
buffer_write(buffer, buffer_s16, argument0); // signal
return buffer;

#define SuperSocket_write
///SuperSocket_write(buffer, buffer_type, value);
if(argument1 == buffer_string)and(global.buffer_mode == false) {
    buffer_write(argument0, buffer_u16, string_length(argument2) + 1);
    buffer_write(argument0, argument1, argument2);
}else {
    // Common
    buffer_write(argument0, argument1, argument2);
}

#define SuperSocket_send
///SuperSocket_send(buffer);
var offset = buffer_tell(argument0);
buffer_seek(argument0, buffer_seek_start, 0);
buffer_write(argument0, buffer_u32, offset);
network_send_raw(sys_network.socket, argument0, offset);
global.check_bytes_send += offset;
buffer_delete(argument0);
global.buffer_mode = false;

#define SuperSocket_read
///SuperSocket_read(buffer, buffer_type);
return buffer_read(argument0, argument1);

#define SuperSocket_status
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

#define SuperSocket_reconnect
///SuperSocket_reconnect();
show_message("re-connect");
SuperSocket_connect(global.reconnect_ip, global.reconnect_port);

#define SuperSocket_isConnected
///SuperSocket_isConnected();
if(instance_exists(sys_network)) {
    return sys_network.status;
}else{
    return -1;
}

#define only_single
///only_single();
var check = false;
with(object_index) {
    if(id != other.id) {
        check = true;
    }
}
if(check)
    instance_destroy();

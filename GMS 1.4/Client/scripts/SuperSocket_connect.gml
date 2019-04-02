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

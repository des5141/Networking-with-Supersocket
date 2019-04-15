///SuperSocket_send(buffer);
var offset = buffer_tell(argument0);
buffer_seek(argument0, buffer_seek_start, 0);
buffer_write(argument0, buffer_u32, offset);
network_send_raw(sys_network.socket, argument0, offset);
global.check_bytes_send += offset;
buffer_delete(argument0);
global.buffer_mode = false;

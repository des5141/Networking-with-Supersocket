///SuperSocket_send(buffer, Type, SendTo, Space);
var offset = buffer_tell(argument0);
buffer_seek(argument0, buffer_seek_start, 0);
buffer_write(argument0, buffer_u16, offset); // Size
buffer_write(argument0, buffer_u8, argument1); // SendTo
buffer_write(argument0, buffer_u16, argument2); // Type
buffer_write(argument0, buffer_u16, argument3); // Space
network_send_raw(sys_network.socket, argument0, offset);
global.check_bytes_send += offset;
buffer_delete(argument0);
global.buffer_mode = false;

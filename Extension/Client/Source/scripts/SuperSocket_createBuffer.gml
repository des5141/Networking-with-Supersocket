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

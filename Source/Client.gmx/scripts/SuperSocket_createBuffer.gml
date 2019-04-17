///SuperSocket_createBuffer(Size);

if(argument1 == SuperSocket.SendToClient){
    global.buffer_mode = true;
}

var buffer = buffer_create(argument0, buffer_grow, 1);
buffer_write(buffer, buffer_u32, 0); // Size
buffer_write(buffer, buffer_u8, 0); // sendTo
buffer_write(buffer, buffer_u16, 0); // space
buffer_write(buffer, buffer_u16, 0); // signal
return buffer;

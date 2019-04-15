///SuperSocket_write(buffer, buffer_type, value);
if(argument1 == buffer_string)and(global.buffer_mode == false) {
    buffer_write(argument0, buffer_u16, string_length(argument2) + 1);
    buffer_write(argument0, argument1, argument2);
}else {
    // Common
    buffer_write(argument0, argument1, argument2);
}

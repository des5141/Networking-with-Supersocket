///SuperSocket_isConnected();
if(instance_exists(sys_network)) {
    return sys_network.status;
}else{
    return -1;
}

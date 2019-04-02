///SuperSocket_init();
global.message_processing = ds_queue_create();
global.message_buffer = -1;
global.check_bytes_recv = 0;
global.check_bytes_send = 0;
global.reconnect_ip = "";
global.reconnect_port = 9999;
network_set_config(network_config_connect_timeout, 200);

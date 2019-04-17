///only_single();
var check = false;
with(object_index) {
    if(id != other.id) {
        check = true;
    }
}
if(check)
    instance_destroy();

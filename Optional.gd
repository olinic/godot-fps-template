class_name Optional
extends RefCounted

var _value: Variant
var _is_present: bool = false

static func of(value: Variant) -> Optional:
	var opt = Optional.new()
	opt._value = value
	opt._is_present = true
	return opt

static func empty() -> Optional:
	var opt = Optional.new()
	opt._is_present = false
	return opt

func get_value() -> Variant:
	assert(_is_present, "Optional is empty")
	return _value

func or_else(other: Variant) -> Variant:
	return _value if _is_present else other

func is_present() -> bool:
	return _is_present

func if_present(callback: Callable) -> void:
	if _is_present:
		callback.call(_value)

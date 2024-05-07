class_name Optional
extends RefCounted

var _is_present: bool = false
var _value: Variant:
	get:
		return _value
	set(value):
		self._is_present = true
		_value = value

static func of(value: Variant) -> Optional:
	var opt = Optional.new()
	opt._value = value
	return opt

static func empty() -> Optional:
	return Optional.new()

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

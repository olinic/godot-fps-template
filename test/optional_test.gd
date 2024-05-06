# GdUnit generated TestSuite
class_name OptionalTest
extends GdUnitTestSuite
@warning_ignore('unused_parameter')
@warning_ignore('return_value_discarded')

var success = false

func before_test():
	self.success = false

func test_optional_of():
	var opt = Optional.of(1)
	assert_int(opt.get_value()).is_equal(1)

func test_optional_empty():
	var opt = Optional.empty()
	assert_bool(opt.is_present()).is_false()

func test_optional_if_present_no_value():
	var opt = Optional.empty()
	var callback = func(val):
		self.success = true
	opt.if_present(callback)
	assert_bool(success).is_false()

func test_optional_if_present():
	var opt = Optional.of(1)
	var callback = func(val):
		self.success = true
	opt.if_present(callback)
	assert_bool(success).is_true()
	
func test_or_else_with_value():
	var opt = Optional.of(2)
	assert_int(opt.or_else(1)).is_equal(2)

func test_or_else_empty():
	var opt = Optional.empty()
	assert_int(opt.or_else(1)).is_equal(1)

file_ = open('Test.txt', 'w')
file_.write('whatever')
file_.close()

file_ = open('Test.txt', 'r')	
print(file_.read())
import http.client
import requests
import sys
import time

alphabet = "0123456789"
alphabetString = "0-9"
if(len(sys.argv) > 1):
	if(sys.argv[1] == "2"):
		alphabet = "abcdefghijklmnopqrstuvwxyz"
		alphabetString = "a-z"
	elif(sys.argv[1] == "3"):
		alphabet = "0123456789abcdefghijklmnopqrstuvwxyz"
		alphabetString = "0-9a-z"
	elif(sys.argv[1] == "4"):
		alphabet = "0123456789abcdefghijklmnopqrstuvwxyz!+,-.:;?_"
		alphabetString = "0-9a-z!+,-.:;?_"
status = ""
startTimestamp = None
lastAttemptMemory = ""
attemptCountMemory = 0
session = requests.Session()
successAttempt = ""
printTimestamp = int(time.time() * 1000)

def pad(n, z=2):
	return ("00" + n)[-z:]

def getTime(inputMs):
	s = int(inputMs)
	ms = s % 1000
	s = int((s - ms) / 1000)
	secs = s % 60
	s = int((s - secs) / 60)
	mins = s % 60
	hrs = int((s - mins) / 60)

	if(inputMs < (60 * 60 * 1000)):
		return str(mins) + ":" + pad(str(secs))
	else:
		return str(hrs) + ":" + pad(str(mins)) + ":" + pad(str(secs))

def showStats():
	global alphabetString, status, startTimestamp, attemptCountMemory, lastAttemptMemory, successAttempt

	print("--------------------------------")
	print("Alphabet: " + alphabetString)
	print("Server status: " + status)
	if(startTimestamp):
		print("Total time: " + getTime(int(time.time() * 1000) - startTimestamp))
	else:
		print("Total time: " + getTime(0))
	print("Attempt count: " + str(attemptCountMemory))
	print("Last attempt: " + lastAttemptMemory)
	print("Cracked password: " + successAttempt)

def nextAttempt(lastAttempt=""):
	global alphabet

	if(len(lastAttempt) == 0):
		lastAttempt = alphabet[0]
	else:
		for i in range(len(lastAttempt) - 1, -1, -1):
			# Current position character up
			if(alphabet.index(lastAttempt[i]) + 1 < len(alphabet)):
				lastAttempt = lastAttempt[0:i] + alphabet[alphabet.index(lastAttempt[i]) + 1] + lastAttempt[i + 1:]
				break
			# Current position character down to beginning of alphabet
			elif(i > 0):
				lastAttempt = lastAttempt[0:i] + alphabet[0] + lastAttempt[i + 1:]
			# Add position + Current position character down to beginning of alphabet
			else:
				lastAttempt = alphabet[0] + lastAttempt[0:i] + alphabet[0] + lastAttempt[i + 1:]
	return lastAttempt

def sendAttempts(difficulty, lastAttempt=""):
	global status, lastAttemptMemory, attemptCountMemory, successAttempt, printTimestamp

	while(True):
		lastAttemptMemory = lastAttempt
		attemptCountMemory += 1

		formData = {
			"difficulty": str(difficulty),
			"password": str(lastAttempt)
		}

		response = session.post("http://localhost/server.php", data = formData)
		if(response.status_code >= 200 and response.status_code < 300):
			if(response.text == "OK"):
				successAttempt = lastAttempt
				showStats()
				break
			else:
				lastAttempt = nextAttempt(lastAttempt)

				# Print stats once a second
				if(printTimestamp < int(time.time() * 1000) - 1000):
					showStats()
					printTimestamp = int(time.time() * 1000)
		else:
			status = str(response.status_code) + " " + http.client.responses[response.status_code]
			showStats()
			break

def crack():
	global status, lastAttemptMemory, attemptCountMemory, successAttempt, startTimestamp

	status = "200 OK"
	lastAttemptMemory = ""
	attemptCountMemory = 0
	successAttempt = ""
	startTimestamp = int(time.time() * 1000)
	showStats()

	sendAttempts(int(sys.argv[1]))

# Run options
if(len(sys.argv) > 2):
	# Send form
	response = requests.post("http://localhost/server.php", data = {
		"difficulty": str(sys.argv[1]),
		"password": str(sys.argv[2])
	})
	print(response.text)
elif(len(sys.argv) > 1):
	# Crack
	crack()
else:
	print("--------------------------------")
	print("This app contains 2 features:")
	print("1) Cracking a password:     python3 crack.py [difficulty]")
	print("2) Manually sending a form: python3 crack.py [difficulty] [password]")
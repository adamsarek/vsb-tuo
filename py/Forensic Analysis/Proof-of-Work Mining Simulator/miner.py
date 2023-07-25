import hashlib
import random

def hash_fn(data):
	return hashlib.md5(str(data).encode("UTF-8")).hexdigest()

def block_hash_found(difficulty, potential_block_hash):
	return (potential_block_hash[0:difficulty]) == ("0" * difficulty)

def get_block_hash(prev_block_hash, data, nonce):
	return hash_fn(prev_block_hash + data + str(nonce))

def main(difficulty):
	block_hash = "-"

	for block_id in range(0, 5):
		print("Mining block #{}, difficulty: {}".format(block_id, difficulty))
		
		prev_block_hash = block_hash
		data_raw = random.randint(0, 10_000_000)
		data = hash_fn(data_raw)
		nonce = 0

		# Guess nonce
		while True:
			potential_block_hash = get_block_hash(prev_block_hash, data, nonce)
			if block_hash_found(difficulty, potential_block_hash):
				block_hash = potential_block_hash
				break
			nonce += 1
		
		# Block details
		print(
			"\nBlock Height: {}\nCurrent Block Hash: {}\nPrevious Block Hash: {}\nRaw Data: {}\nData: {}\nNonce: {}\n".format(
			block_id,
			block_hash,
			prev_block_hash,
			data_raw,
			data,
			nonce
		))

		# Not a part of mining
		difficulty += 1

# Start
main(2)
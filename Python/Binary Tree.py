#########################
# Binary Tree Structure #
#########################

class BinaryTree:
	def __init__(self, data):
		self.data = data
		self.left = None
		self.right = None

	def display_tree(self, level=0, prefix="Root: "):
		if root is not None:
			if level == 0:
				print(f"{prefix}{str(self.data)}")
			else:
				line = " " * (level * 4) + "|-- "
				if prefix.startswith("L: "):
					line = " " * (level * 4) + "|--L: "
				elif prefix.startswith("R: "):
					line = " " * (level * 4) + "|--R: "
				print(line + str(self.data))

			if self.left is not None:
				self.left.display_tree(level + 1, "L: ")
			if self.right is not None:
				self.right.display_tree(level + 1, "R: ")

	# Recursive
	def inorder_traversal(self):
		# left --> root --> right
		result = []

		if self is None:
			return result

		if self.left:
			result.extend(self.left.inorder_traversal())

		result.append(self.data)

		if self.right:
			result.extend(self.right.inorder_traversal())

		return result

	# Iterative
	def iterative_inorder_traversal(self):
		# left --> root --> right
		if self is None:
			return []

		result = []
		stack = []
		current = self

		while current or stack:
			while current:
				stack.append(current)
				current = current.left

			current = stack.pop()
			result.append(current.data)
			current = current.right

		return result


	# Recursive
	def preorder_traversal(self):
		# root --> left --> right
		if self is None:
			return []

		result = []

		result.append(self.data)

		if self.left:
			result.extend(self.left.preorder_traversal())

		if self.right:
			result.extend(self.right.preorder_traversal())

		return result

	# Iterative
	def iterative_preorder_traversal(self):
		# root --> left --> right
		if self is None:
			return []

		stack = []
		result = []
		current = self

		while current or stack:
			while current:
				result.append(current.data)
				stack.append(current)
				current = current.left

			current = stack.pop()
			current = current.right

		return result

	
	# Recursive
	def postorder_traversal(self):
		# left --> right --> root
		if self is None:
			return []

		result = []

		if self.left:
			result.extend(self.left.postorder_traversal())

		if self.right:
			result.extend(self.right.postorder_traversal())

		result.append(self.data)

		return result

	# Iterative
	def iterative_postorder_traversal(self):
		# left --> right --> root
		if self is None:
			return []

		stack = []
		result = []
		current = self
		prev = None

		while current or stack:
			while current:
				stack.append(current)
				current = current.left

			current = stack[-1]

			if current.right is None or current.right == prev:
				result.append(current.data)
				stack.pop()
				prev = current
				current = None
			else:
				current = current.right

		return result

	def is_same_tree(self, p, q):
		if p is None and q is None:
			return True

		if p is None or q is None:
			return False

		return p.data == q.data and self.is_same_tree(p.left, q.left) and self.is_same_tree(p.right, q.right)


	def is_symmetric(self):
		def is_mirror(left, right):
			if not left and not right:
				return True
			if not left or not right:
				return False
			return (left.data == right.data) and is_mirror(left.left, right.right) and is_mirror(left.right, right.left)

		if not self:
			return True

		return is_mirror(self.left, self.right)


	def is_balanced(self):
		def check_balance(node):
			# An empty tree is balanced and has a height of 0.
			if not node:
				return True, 0

			# Check if the left subtree is balanced and get its height.
			is_left_balanced, left_height = check_balance(node.left)
			if not is_left_balanced:
				return False, 0
				# If the left subtree is not balanced, the whole tree is unbalanced.

			is_right_balanced, right_height = check_balance(node.right)
			if not is_right_balanced:
				return False, 0

			is_current_balanced = abs(left_height - right_height) <= 1
			current_height = max(left_height, right_height) + 1

			return is_current_balanced, current_height

		is_balanced, _ = check_balance(self)
		return is_balanced


	def min_depth(self):
		# Breadth First Search
		if not self:
			return 0

		queue = [(root, 1)]

		while queue:
			node, depth = queue.pop(0)

			if not node.left and not node.right:
				return depth

			if node.left:
				queue.append((node.left, depth + 1))


			if node.right:
				queue.append((node.right, depth + 1))


	def max_depth(self):
		stack = [[self, 1]]
		result = 0

		while stack:
			node, depth = stack.pop()

			if node:
				result = max(result, depth)
				stack.append([node.left, depth + 1])
				stack.append([node.right, depth + 1])
		return result


	def has_path_sum(self, root, target_sum):
		if root is None:
			return False

		target_sum -= root.data

		if root.left is None and root.right is None:
			return target_sum == 0

		return self.has_path_sum(root.left, target_sum) or self.has_path_sum(root.right, target_sum)


	def largest_values(self):
		if not self:
			return []

		result = []
		queue = [self]

		while queue:
			level_max = float('-inf')
			level_size = len(queue)

			for i in range(level_size):
				node = queue.pop(0)
				level_max = max(level_max, node.data)

				if node.left:
					queue.append(node.left)

				if node.right:
					queue.append(node.right)

			result.append(level_max)

		return result


	def binary_tree_paths(self):
		def dfs(node, current_path):
			if not node:
				return

			current_path.append(str(node.data))

			if not node.left and not node.right:
				result.append("-> ".join(current_path))

			dfs(node.left, current_path)
			dfs(node.right, current_path)

			current_path.pop()

		result = []
		dfs(self, [])
		return result

	def count_nodes(self):
	    if not self:
	        return 0

	    depth_left, depth_right = 0, 0
	    left = right = self

	    while left:
	        left = left.left
	        depth_left += 1

	    while right:
	        right = right.right
	        depth_right += 1

	    if depth_left == depth_right:
	        return 2 ** depth_left - 1
	    else:
	        left_count = self.left.count_nodes() if self.left else 0
	        right_count = self.right.count_nodes() if self.right else 0
	        return 1 + left_count + right_count

	def sum_of_left_leaves(self):
	    if not self:
	        return 0

	    left_sum = 0

	    if self.left and not self.left.left and not self.left.right:
	        left_sum += self.left.data

	    left_sum += self.left.sum_of_left_leaves() if self.left else 0
	    left_sum += self.right.sum_of_left_leaves() if self.right else 0

	    return left_sum

	def level_order(self):
		if not self:
			return []

		result = []
		queue = [self]

		while queue:
			level = []
			level_size = len(queue)

			for _ in range(level_size):
				node = queue.pop(0)
				level.append(node.data)

				if node.left:
					queue.append(node.left)

				if node.right:
					queue.append(node.right)

			result.append(level)
		return result
		

#    5
#   / \
#  6   8
#   \   \
#    9   11
#   / \
#  14  15


# Main Root
root = BinaryTree(5)

# 1st Level
root.left = BinaryTree(6)
root.right = BinaryTree(8)

# 2nd Level
root.left.right = BinaryTree(9) 
root.right.right = BinaryTree(11)

# 3rd Level
root.left.right.left = BinaryTree(14)
root.left.right.right = BinaryTree(15)


print("**Binary Tree Sturcture**", "\n")
# InOrder Traversal
print("InOrder Recursive:", root.inorder_traversal())
print("InOrder Iterative:", root.iterative_inorder_traversal())
print("\r")
# PreOrder Traversal
print("PreOrder Recursive:", root.preorder_traversal())
print("PreOrder Iterative:", root.iterative_preorder_traversal())
print("\r")
# PostOrder Traversal
print("PostOrder Recursive:", root.postorder_traversal())
print("PostOrder Iterative:", root.iterative_postorder_traversal())
print("\n")


# Comparisons
# P - Tree
p_root = BinaryTree(1)
p_root.left = BinaryTree(2)
p_root.right = BinaryTree(3)

# Q - Tree
q_root = BinaryTree(1)
q_root.left = BinaryTree(2)
q_root.right = BinaryTree(3)

p_root.display_tree()
q_root.display_tree()

print(p_root.is_same_tree(p_root, q_root))


# Is Symmetric?
#          1
# 		 /   \
# 		2     2
#      / \   / \
#     3   4 4   3


# Main Root
symm_root = BinaryTree(1)

# 1st Level
symm_root.left = BinaryTree(2)
symm_root.right = BinaryTree(2)

# 2nd Level
symm_root.left.left = BinaryTree(3)
symm_root.right.right = BinaryTree(3)
symm_root.left.right = BinaryTree(4)
symm_root.right.left = BinaryTree(4)

print("Symmetric:", symm_root.is_symmetric())

# Is Balanced?
#  - a tree is balance if left & right has <= 1 height difference.
print("Balanced:", symm_root.is_balanced())

# Minimum Depth till a leaf node is met.
print("Min Depth:", symm_root.min_depth())

# Maximum Depth till a leaf node is met.
print("Max Depth:", symm_root.max_depth())


# Path Sum
#        5
#       / \
#      4   8
#     /   / \
#    11  13  4
#   /  \      \
#  7    2      1

# 5 + 4 + 11 + 2 = 22
# target_sum = 22

# Main Root
root_sum = BinaryTree(5)

# 1st Level
root_sum.left = BinaryTree(4)
root_sum.right = BinaryTree(8)

# 2nd Level
root_sum.left.left = BinaryTree(11) 
root_sum.right.left = BinaryTree(13) 
root_sum.right.right = BinaryTree(4) 

# 3rd Level
root_sum.left.left.left = BinaryTree(7)
root_sum.left.left.right = BinaryTree(2)
root_sum.right.right.right = BinaryTree(1)

# Will return True
print(root_sum.has_path_sum(root_sum, 22))

print(root_sum.largest_values())

print(root_sum.binary_tree_paths())



#  Complete Tree
#         1
#        / \
#		2 	3
# 	   /\  /
# 	  4  5 6


# Main Root
complete_root = BinaryTree(1)

# 1st Level
complete_root.left = BinaryTree(2)
complete_root.right = BinaryTree(3)

# 2nd Level
complete_root.left.left = BinaryTree(4)
complete_root.left.right = BinaryTree(5)
complete_root.right.left = BinaryTree(6)

# Counts complete tree nodes
print("Nodes: ", complete_root.count_nodes())

# Sum of all left leaf nodes
#  4 + 6 = 10
print("Left leaf sum: ", complete_root.sum_of_left_leaves())


# Level Order Traversal
print("Level Order Traversal:", complete_root.level_order())
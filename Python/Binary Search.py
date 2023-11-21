################################
# Binary Search Tree Structure #
################################

class BSTree:
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


	def increasing_BST(self):
		def in_order_traversal(node):
			if not node:
				return []

			return in_order_traversal(node.left) + [node] + in_order_traversal(node.right)

		sorted_nodes = in_order_traversal(self)

		new_root = BSTree(sorted_nodes[0].data)
		current_node = new_root

		for node in sorted_nodes[1:]:
			current_node.right = BSTree(node.data)
			current_node = current_node.right

		self = new_root
		while self:
			if self.right is None:
				print(self.data)
			else:
				print(self.data, end=" --> ")
			self = self.right

	def searchBST(self, data):
		if not self:
			return self

		if self.data == data:
			return self

		elif self.data < data:
			return self.right.searchBST(data)

		else:
			return self.left.searchBST(data)


	def find_mode(self):
		def in_order_traversal(node):
			nonlocal max_frequency

			if node is None:
				return

			in_order_traversal(node.left)

			value = node.data
			freq_count[value] = freq_count.get(value, 0) + 1

			max_frequency = max(max_frequency, freq_count[value])

			in_order_traversal(node.right)

		freq_count = {}
		max_frequency = 0
		in_order_traversal(self)

		modes = [key for key, value in freq_count.items() if value == max_frequency]

		return modes


	def get_minimum_diff(self):
		def in_order_traversal(node):
			nonlocal prev, min_diff

			if not node:
				return

			in_order_traversal(node.left)

			if prev is not None:
				min_diff = min(min_diff, abs(node.data - prev))

			prev = node.data
			in_order_traversal(node.right)

		prev = None
		min_diff = float('inf')
		in_order_traversal(self)

		return min_diff


	def lowest_common_ancestor(self, p, q):
		while self:
			if p.data < self.data and q.data < self.data:
				self = self.left
			elif p.data > self.data and q.data > self.data:
				self = self.right
			else:
				return self.data
		return None


def find_min(node):
    while node.left:
        node = node.left
    return node.data

def delete_node(root, key):
    if not root:
        return root

    if key < root.data:
        root.left = delete_node(root.left, key)
    elif key > root.data:
        root.right = delete_node(root.right, key)
    else:
        if not root.left:
            return root.right
        elif not root.right:
            return root.left

        min_val = find_min(root.right)
        root.data = min_val
        root.right = delete_node(root.right, min_val)
    return root


def sorted_array_to_BST(nums):
	if not nums:
		return None

	def constructBST(left, right):
		if left > right:
			return None

		mid = left + (right - left) // 2
		root = BSTree(nums[mid])

		root.left = constructBST(left, mid - 1)
		root.right = constructBST(mid + 1, right)

		return root

	return constructBST(0, len(nums) - 1)


#             5
# 			 / \
#			3   6
# 		   / \   \
#  		  2   4   8
# 		 /		  /\
# 		1   	 7	9	

# Main Root
root = BSTree(5)

# 1st Level
root.left = BSTree(3)
root.right = BSTree(6)

# 2nd Level
root.left.left = BSTree(2)
root.left.right = BSTree(4)
root.right.right = BSTree(8)

# 3rd Level
root.left.left.left = BSTree(1)
root.right.right.left = BSTree(7)
root.right.right.right = BSTree(9)

# Aligns all nodes to right (a.k.a Linkedlist)
new_root = root.increasing_BST()


#   Basic Search 
#          9
# 		  / \
#        5   14
#       / \    \
#      3   7    18
#               / \
#              16  25


# Main Root
find_root = BSTree(9)

# 1st Level
find_root.left = BSTree(5)
find_root.right = BSTree(14)

# 2nd Level
find_root.left.left = BSTree(3)
find_root.left.right = BSTree(7)
find_root.right.right = BSTree(18)

# 3rd Level
find_root.right.right.left = BSTree(16)
find_root.right.right.right = BSTree(25)

# Basic Search
new_tree = find_root.searchBST(18)
new_tree.display_tree()

#  Find Modes
#        6
#       / \
#      4   9
#     / \   \
#    4   5   9
#   /    /    \
#  4    3      9


# Main Root
root_modes = BSTree(6)

# 1st Level
root_modes.left = BSTree(4)
root_modes.right = BSTree(9)

# 2nd Level
root_modes.left.left = BSTree(4)
root_modes.left.right = BSTree(5)
root_modes.right.right = BSTree(9)

# 3rd Level
root_modes.left.left.left = BSTree(4)
root_modes.left.right.left = BSTree(3)
root_modes.right.right.right = BSTree(9)

#  Return the nodes that appeared the most
print("Tree Modes:", root_modes.find_mode())

print("Minimum Diff:", root_modes.get_minimum_diff())

#  Lowest Common Ancestor between 2 nodes
p = root_modes.left.left.left
q = root_modes.left.right.left
print("LCA:", root_modes.lowest_common_ancestor(p, q))


#  Delete a Node
#         5
#        / \
#       3   6
#      / \   \
#     2   4   7


# Main Root
root_del = BSTree(5)

# 1st Level
root_del.left = BSTree(3)
root_del.right = BSTree(6)

# 2nd Level
root_del.left.left = BSTree(2)
root_del.left.right = BSTree(4)
root_del.right.right = BSTree(7)

print("\n", "Before Deletion:")
root_del.display_tree()

delete_node(root_del, 3)

print("\n", "After Deletion:")
root_del.display_tree()


# Convert an array to BST
nums = [-10, -3, 0, 5, 9]
nums_root = sorted_array_to_BST(nums)

print("\n", f"From Sorted List: {nums}")
nums_root.display_tree()
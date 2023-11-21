########################
# N-ary Tree Structure #
########################

class N_Ary_Tree:
	def __init__(self, data):
		self.data = data
		self.children = []

	def add_child(self, child):
		self.children.append(child)

	# Recursive
	def preorder_traversal(self):
		# root --> children
		if self is None:
			return []

		result = []

		result.append(self.data)

		if self.children:
			for child in self.children:
				result.extend(child.preorder_traversal())

		return result

	# Iterative
	def iterative_preorder_traversal(self):
		# root --> children
		if self is None:
			return []

		result = []
		stack = [self]

		while stack:
			current = stack.pop()
			result.append(current.data)

			for child in reversed(current.children):
				stack.append(child)

		return result


	# Recursive
	def postorder_traversal(self):
		# children --> root
		if self is None:
			return []

		result = []

		if self.children:
			for child in self.children:
				result.extend(child.postorder_traversal())

		result.append(self.data)

		return result

	# Iterative
	def iterative_postorder_traversal(self):
		# children --> root
		if self is None:
			return []
		
		result = []
		stack = [self]

		while stack:
			current = stack.pop()
			result.append(current.data)

			for child in current.children:
				stack.append(child)

		return result[::-1]


	def max_depth(self):
		if not self:
			return 0

		if not self.children:
			return 1

		max_child_depth = 0
		for child in self.children:
			child_depth = child.max_depth()
			max_child_depth = max(max_child_depth, child_depth)

		return max_child_depth + 1


	def level_order(self):
		if not self:
			return []

		result = []
		queue = [self]

		while queue:
			level_nodes = []

			for _ in range(len(queue)):
				node = queue.pop(0)
				level_nodes.append(node.data)
				queue.extend(node.children)
			result.append(level_nodes)

		return result


	def diameter_of_Ntree(self):
		def dfs(node):
			nonlocal diameter
			if not node:
				return 0

			heights = []

			for child in node.children:
				height = dfs(child)
				heights.append(height)

			if heights:
				# Sort the heights of children in decreasing order
				heights.sort(reverse=True)

				# Calculate the diameter of the current subtree
				if len(heights) >= 2:
					diameter = max(diameter, heights[0] + heights[1] + 1)

			#  Return the height of the current subtree
			return max(heights, default=0) + 1

		diameter = 0
		dfs(self)
		return diameter


#            1
#          / | \
# 		  3  2  4
#		 / \     \
# 	    5   6     8
#      /          /\
# 	  9         10  11

# Main Root
root = N_Ary_Tree(1)

# 1st Level
node_a = N_Ary_Tree(3)
node_b = N_Ary_Tree(2)
node_c = N_Ary_Tree(4)

root.add_child(node_a)
root.add_child(node_b)
root.add_child(node_c)

# 2nd Level
node_d = N_Ary_Tree(5)
node_e = N_Ary_Tree(6)
node_f = N_Ary_Tree(8)

node_a.add_child(node_d)
node_a.add_child(node_e)
node_c.add_child(node_f)

# 3rd Level
node_g = N_Ary_Tree(9)
node_h = N_Ary_Tree(10)
node_i = N_Ary_Tree(11)

node_d.add_child(node_g)
node_f.add_child(node_h)
node_f.add_child(node_i)


print("**N-Ary Tree Sturcture**", "\n")
print("PreOrder Recursive:", root.preorder_traversal())
print("PreOrder Iterative:", root.iterative_preorder_traversal())
print("\r")
print("PostOrder Recursive:", root.postorder_traversal())
print("PostOrder Iterative:", root.iterative_postorder_traversal())
print("\r")

print("Max Depth:", root.max_depth())
print("Level Order:", root.level_order())
print("Diameter:", root.diameter_of_Ntree())


#              7
#           / | | \
#         10 20 30 40
#         /   \
#       50    60
#      / |      \
#     90 100    70
#                 \
#                 110

# Main Root
root2 = N_Ary_Tree(7)

# 1st Level
node_1 = N_Ary_Tree(10)
node_2 = N_Ary_Tree(20)
node_3 = N_Ary_Tree(30)
node_4 = N_Ary_Tree(40)

root2.add_child(node_1)
root2.add_child(node_2)
root2.add_child(node_3)
root2.add_child(node_4)

# 2nd Level
node_5 = N_Ary_Tree(50)
node_6 = N_Ary_Tree(60)

node_1.add_child(node_5)

# 3rd Level
node_7 = N_Ary_Tree(90)
node_8 = N_Ary_Tree(100)
node_9 = N_Ary_Tree(70)

node_5.add_child(node_7)
node_5.add_child(node_8)
node_2.add_child(node_6)
node_6.add_child(node_9)

# 4th Level
node_10 = N_Ary_Tree(110)

node_9.add_child(node_10)

print("Diameter:", root2.diameter_of_Ntree())
using System.Collections.Generic;

namespace LeetCode_Add2Number
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        [DataRow("243", "564", "807")]
        [DataRow("0", "0", "0")]
        [DataRow("9999999", "9999", "10009998")]
        public void TestMethod1(string a, string b, string result)
        {
            var r_node = AddTwoNumbers(CreateListNode(a), CreateListNode(b));
        }

        private ListNode CreateListNode(string str)
        {
            ListNode node = new ListNode();

            var reader = str.ToCharArray().Reverse().GetEnumerator();

            if (reader.MoveNext()) {
                SetNextNode(node, reader);
            }
                       

            return node;
        }

        private void SetNextNode(ListNode node, IEnumerator<char> reader)
        {
            node.val = Convert.ToInt16(reader.Current.ToString());
            if (reader.MoveNext())
            {
                node.next = new ListNode();
                SetNextNode(node.next, reader);
            }
        }

        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {            
            Queue<ListNode> l1_stack = new Queue<ListNode>();
            Queue<ListNode> l2_stack = new Queue<ListNode>();
            Queue<ListNode> l3_collect = new Queue<ListNode>();
            ListNode result = new ListNode();

            //將l1與l2取值到最後一個元素，並逐一加入stack
            ListNode currentNode = l1;
            do
            {                
                l1_stack.Enqueue(currentNode);
                currentNode = currentNode.next;
            } while (currentNode != null);

            currentNode = l2;
            do
            {
                l2_stack.Enqueue(currentNode);
                currentNode = currentNode.next;
            }
            while (currentNode != null); 

            //只要其中一個stack有元素在駐列中，就要逐一取出，然後將相加的值產生一個元素，然後加入queue的駐列中
            int carry = 0;
            while (l1_stack.Any() || l2_stack.Any() || carry > 0)
            {
                ListNode ls1 = null;
                ListNode ls2 = null;

                if (l1_stack.Any()) 
                {
                    ls1 = l1_stack.Dequeue();
                }

                if (l2_stack.Any()) 
                {
                    ls2 = l2_stack.Dequeue();
                }
                
                ListNode node_sum = new ListNode();
                //取得相加的值，如果有進位就只留個位數，另外需要將前一個元素的進位值一併加入(如果沒有的情況下會是0)
                int nodeSumInt = GetNodeVal(ls1) + GetNodeVal(ls2) + carry;
                if (nodeSumInt >= 10) 
                {
                    char digit = nodeSumInt.ToString()[1];
                    node_sum.val = Convert.ToInt16(digit.ToString());
                    carry = 1;
                }
                else
                {
                    node_sum.val = nodeSumInt;
                    carry = 0;
                }

                l3_collect.Enqueue(node_sum);
            }

            result = l3_collect.Dequeue();
            if (!l3_collect.Any())
            {
                return result;
            }

            ListNode next1 = l3_collect.Dequeue();
            result.next= next1;
            while (l3_collect.Any())
            {
                ListNode next2 = l3_collect.Dequeue();                
                next1.next = next2;
                next1 = next2;
            }

            return result;
        }

        private int GetNodeVal(ListNode? ls1)
        {
            if (ls1 == null)
                return 0;
            else
                return ls1.val;
        }

        public ListNode GetNextNode(ListNode node) 
        {
            if (node != null)
                return node.next;
            else
                return null;
        }

        private ulong ConvertToInt(ListNode l1)
        {
            ulong result = 0;
            string result_str = string.Empty;

            do
            {
                result_str += l1.val;
                l1 = l1.next;
            }
            while (l1 != null);

            result_str = new string(result_str.Reverse().ToArray());
            result = Convert.ToUInt64(result_str);
            return result;
        }
    }
}
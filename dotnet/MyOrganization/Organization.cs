using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;

        public Organization()
        {
            root = CreateOrganization();
            PullTitles(root);
            var test = titles.Distinct();
        }

        private List<Employee> Employees = new List<Employee>();
        private List<string> titles = new List<string>();

        protected abstract Position CreateOrganization();

        /**
         * hire the given person as an employee in the position that has that title
         * 
         * @param person
         * @param title
         * @return the newly filled position or empty if no position has that title
         */
        public Position? Hire(Name person, string title)
        {
            Position? pos = null;
            if(!titles.Contains(title))
            {
                return null;
            }
            Random random = new Random();
            var emp = new Employee(random.Next(), person);
            
            
            if(root.GetTitle() == title)
            {
                root.SetEmployee(emp);
                return root;
            }
            else
            {
                pos = FindAndFetch(root, title, emp);
            }
            return pos;
        }


        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "\t"));
            }
            return sb.ToString();
        }

        private void PullTitles(Position root)
        {
            titles.Add(root.GetTitle());
            foreach (Position p in root.GetDirectReports())
            {
                titles.Add(p.GetTitle());
                PullTitles(p);
            }
        }

        private Position? FindAndFetch(Position root, string title, Employee emp)
        {
            Position? temp = null;
            foreach (var position in root.GetDirectReports())
            {
                if(position.GetTitle() == title)
                {
                    position.SetEmployee(emp);
                    return position;
                }
                else
                {
                    FindAndFetch(position,title,emp);
                }
            }
            return temp;
        }
    }
}

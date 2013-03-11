using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ArtData
{
    public class BaseForm  : Form
    {
        private DialogForm _dialogForm;
        public DialogResult DResult;
        public string DText;

        public DialogResult ShowTextDialog()
        {
            _dialogForm = new DialogForm();
            _dialogForm.ShowDialog(this);
            return DResult;
        }

        
    }
}

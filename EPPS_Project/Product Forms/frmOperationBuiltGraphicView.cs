using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using static ProductionPlanning.MyEnums;

namespace ProductionPlanning
{
    public partial class frmOperationBuiltGraphicView
    {
        public frmOperationBuiltGraphicView()
        {
            mDiagramPrintDocument = new PrintDocument();
            InitializeComponent();
            _pbDiagram.Name = "pbDiagram";
            _cmdPrintDiagram.Name = "cmdPrintDiagram";
            _cmdExit.Name = "cmdExit";
        }

        private DataSetConfiguration DataSetConfig = new DataSetConfiguration();
        private DataSet mdsGraphicView;
        private DataRow mCurrentRow;
        private TreeNode mCurrentNode;
        private const int PATHS_BETWEEN_SPACE = 200;
        private const int OBJECTS_SIZE_WIDTH = 100;
        private const int OBJECTS_SIZE_HEIGHT = 100;
        private const int OBJECTS_BETWEEN_SPACE = 75;
        private const int OBJECTS_FIRSTSTEP_SPACE = 50;
        private const int OBJECTS_FIRSTMATERIALS_STARTSPACE = 20;
        private const int OBJECTS_MIDLLEMATERIALS_STARTSPACE = 20;
        private const int OBJECTS_MATERIALS_BETWEENSPACE = 10;
        private const int GRIDSIZE = 16;
        private const int SCREEN_GROW_STEP = 30;
        private ArrayList DiagramPieces = new ArrayList();
        private ArrayList DiagramArrows = new ArrayList();
        //private int mSelectedIndex = -1;
        //private bool mAddSymbol = false;
        private Rectangle SelRec;
        //private bool EmptyArea = false;
        //private Point OldPoint;
        private string mTreeCode;
        private PrintDocument _mDiagramPrintDocument;

        private PrintDocument mDiagramPrintDocument
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _mDiagramPrintDocument;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_mDiagramPrintDocument != null)
                {
                    _mDiagramPrintDocument.PrintPage -= mDiagramPrintDocument_PrintPage;
                }

                _mDiagramPrintDocument = value;
                if (_mDiagramPrintDocument != null)
                {
                    _mDiagramPrintDocument.PrintPage += mDiagramPrintDocument_PrintPage;
                }
            }
        }

        public string TreeCode
        {
            set
            {
                mTreeCode = value;
            }
        }

        public DataRow CurrentRow
        {
            set
            {
                mCurrentRow = value;
            }
        }

        public TreeNode CurrentNode
        {
            set
            {
                mCurrentNode = value;
            }
        }

        private void frmOperationBuiltGraphicView_Load(object sender, EventArgs e)
        {
            string Query = "Select * From dbo.Tbl_PreOperations Where TreeCode=" + mTreeCode;
            DataSetConfig.FillDataSet("Tbl_PreOperations", "Tbl_PreOperations", Query, "TreeCode", "CurrentOperationCode", "PreOperationCode");
            Query = "Select * From Tbl_OperationMaterials Where TreeCode=" + mTreeCode;
            DataSetConfig.FillDataSet("Tbl_OperationMaterials", "Tbl_OperationMaterials", Query, "TreeCode", "CurrentOperationCode", "MaterialCode");
            Query = "Select * From Tbl_OperationNetworkPaths Where TreeCode=" + mTreeCode;
            DataSetConfig.FillDataSet("Tbl_OperationNetworkPaths", "Tbl_OperationNetworkPaths", Query, "TreeCode", "PathCode", "ItemPriority");
            Query = "Select * From Tbl_ProductOPCs Where TreeCode=" + mTreeCode;
            DataSetConfig.FillDataSet("Tbl_ProductOPCs", "Tbl_ProductOPCs", Query, "TreeCode", "OperationCode");
            mdsGraphicView = DataSetConfig.dsProductionPlanning;
            DrawGraphic();
        }

        private void frmOperationBuiltGraphicView_FormClosing(object sender, FormClosingEventArgs e)
        {
            {
                var withBlock = mdsGraphicView;
                withBlock.RejectChanges();
                withBlock.Relations.Clear();
                for (short I = (short)(withBlock.Tables.Count - 1); I >= 0; I += -1)
                {
                    withBlock.Tables[I].Constraints.Clear();
                    withBlock.Tables[I].Dispose();
                    withBlock.Tables.RemoveAt(I);
                }
            }

            mCurrentRow = null;
            mCurrentNode = null;
            mdsGraphicView.Dispose();
            DataSetConfig = null;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pbDiagram_GotFocus(object sender, EventArgs e)
        {
            Panel1.Focus();
        }

        private void pbDiagram_MouseClick(object sender, MouseEventArgs e)
        {
            // If CheckBounds(New Point(e.X - (e.X Mod GRIDSIZE), e.Y - (e.Y Mod GRIDSIZE))) = -1 Then
            // EmptyArea = True
            // Else
            // EmptyArea = False
            // End If
            Panel1.Focus();
        }

        private void pbDiagram_MouseDown(object sender, MouseEventArgs e)
        {
            // mSelectedIndex = CheckBounds(New Point(e.X, e.Y))

            // If mSelectedIndex > -1 Then
            // OldPoint = New Point(GetSymbol(mSelectedIndex).LocationRectangle.X, GetSymbol(mSelectedIndex).LocationRectangle.Y)
            // Else
            // OldPoint = New Point(e.X, e.Y)
            // End If

            // SelRec = Nothing
        }

        private void pbDiagram_MouseMove(object sender, MouseEventArgs e)
        {
            // If mSelectedIndex > -1 Then
            // Dim region As Rectangle = New Rectangle(e.X - GRIDSIZE, e.Y - GRIDSIZE, GRIDSIZE * 2, GRIDSIZE * 2)

            // GetSymbol(mSelectedIndex).SetLocation(e.X - GRIDSIZE / 2, e.Y - GRIDSIZE / 2)

            // pbDiagram.Invalidate(region)
            // End If
        }

        private void pbDiagram_MouseUp(object sender, MouseEventArgs e)
        {
            // If mSelectedIndex > -1 Then
            // Dim current As Point = New Point(e.X, e.Y)
            // Dim newPoint As Point = New Point(current.X - (current.X Mod GRIDSIZE), current.Y - (current.Y Mod GRIDSIZE))

            // 'If mAddSymbol Then
            // '    If CheckBounds(current) <> -1 Then
            // '        MessageBox.Show("Collision...!?")
            // '        Exit Sub
            // '    End If
            // 'End If

            // If EmptyArea Then
            // GetSymbol(mSelectedIndex).SetLocation(newPoint.X, newPoint.Y)

            // For I As Int16 = 0 To DiagramArrows.Count - 1
            // If GetArrow(I).StartID = GetSymbol(mSelectedIndex).StepID Then
            // 'محاسبه نقطه شروع جدید برای خط ارتباطی با استفاده از نقطه جدید
            // GetArrow(I).SetStartPoint(New Point(newPoint.X + (GetSymbol(mSelectedIndex).LocationRectangle.Width / 2), newPoint.Y + (GetSymbol(mSelectedIndex).LocationRectangle.Height)))
            // End If
            // Next I

            // For I As Int16 = 0 To DiagramArrows.Count - 1
            // If GetArrow(I).EndID = GetSymbol(mSelectedIndex).StepID Then
            // 'محاسبه نقطه پایانی جدید برای خط ارتباطی با استفاده از نقطه جدید
            // GetArrow(I).SetEndPoint(New Point(newPoint.X + (GetSymbol(mSelectedIndex).LocationRectangle.Width / 2), newPoint.Y))
            // End If
            // Next I
            // Else
            // GetSymbol(mSelectedIndex).SetLocation(OldPoint.X, OldPoint.Y)
            // End If

            // Dim SelIndex As Integer = CheckBounds(current)

            // If SelIndex <> -1 Then
            // SelRec = Nothing
            // SelRec = New Rectangle(New Point(GetSymbol(SelIndex).LocationRectangle.X - 5, GetSymbol(SelIndex).LocationRectangle.Y - 5), New Size(25, 25))
            // End If

            // mSelectedIndex = -1
            // End If

            // RefreshScreen:
            // pbDiagram.Cursor = Cursors.Default
            // pbDiagram.Invalidate()
        }

        private void pbDiagram_Paint(object sender, PaintEventArgs e)
        {
            int I;

            // ترسیم خطوط ارتباطی بین عملیات مختلف در نمودار
            var loopTo = DiagramArrows.Count - 1;
            for (I = 0; I <= loopTo; I++)
                GetArrow(I).DrawArrow(e.Graphics);
            // ترسیم نماد نمایش دهنده عملیات مختلف در نمودار
            var loopTo1 = DiagramPieces.Count - 1;
            for (I = 0; I <= loopTo1; I++)
            {
                GetSymbol(I).Draw(e.Graphics);
                if (this.GetSymbol(I).LocationRectangle.X + GRIDSIZE >= pbDiagram.Width)
                {
                    pbDiagram.Width = pbDiagram.Width + SCREEN_GROW_STEP;
                }

                if (this.GetSymbol(I).LocationRectangle.Y + GRIDSIZE >= pbDiagram.Height)
                {
                    pbDiagram.Height = pbDiagram.Height + SCREEN_GROW_STEP;
                }
            }

            if (SelRec != default)
            {
                // New Pen(Color.FromArgb(100, 255, 255, 255))
                e.Graphics.DrawRectangle(Pens.Blue, SelRec);
            }
        }

        private void mDiagramPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            int I;
            e.PageSettings.PaperSize.RawKind = 0;
            e.PageSettings.PaperSize.Height = 200;
            e.PageSettings.PaperSize.Width = 50;

            // ترسیم خطوط ارتباطی بین عملیات مختلف در نمودار
            var loopTo = DiagramArrows.Count - 1;
            for (I = 0; I <= loopTo; I++)
                GetArrow(I).DrawArrow(e.Graphics);
            // ترسیم نماد نمایش دهنده عملیات مختلف در نمودار
            var loopTo1 = DiagramPieces.Count - 1;
            for (I = 0; I <= loopTo1; I++)
                GetSymbol(I).Draw(e.Graphics);
        }

        private void cmdPrintDiagram_Click(object sender, EventArgs e)
        {
            mDiagramPrintDocument.DocumentName = "OPC Diagram";
            var dlg = new PrintDialog();
            dlg.Document = mDiagramPrintDocument;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                mDiagramPrintDocument.Print();
                Close();
            }
        }

        private void DrawGraphic()
        {
            mdsGraphicView.Tables["Tbl_OperationNetworkPaths"].DefaultView.Sort = "PathCode Desc";
            short PathQuantity = Conversions.ToShort(mdsGraphicView.Tables["Tbl_OperationNetworkPaths"].DefaultView[0]["PathCode"]);
            DataRow[] drPathOperations;
            DataRow[] drMaterials;
            DataRow[] drPreOperations;
            DataRow[] drAfterwardOperations;
            Point ptOpDrawPoint;
            Point ptMaDrawPoint;
            string OperationCode;
            int ExistOpIndex;
            ProgressBar1.Value = 0;
            ProgressBar1.Visible = true;
            string Query;
            var Mycnn = new SqlConnection();
            var MyCmd = new SqlCommand();
            var MyDa1 = new SqlDataAdapter();
            var Myds1 = new DataSet();
            Mycnn.ConnectionString = Module1.PlanningCnnStr;
            Query = "SELECT  PathCode , COUNT(OperationCode) AS OpCount FROM Tbl_OperationNetworkPaths WHERE(TreeCode =" + mTreeCode + ") GROUP BY PathCode  ORDER BY COUNT(OperationCode) DESC";
            MyCmd.CommandText = Query;
            MyCmd.Connection = Mycnn;
            MyDa1.SelectCommand = MyCmd;
            MyDa1.Fill(Myds1, "Tbl_OperationPaths");
            object MyPathNo = Myds1.Tables["Tbl_OperationPaths"].Rows.Count;

            // حلقۀ شمارندۀ مسیرهای شبکه عملیات درخت محصول
            // For PathCounter As Int16 = 1 To PathQuantity
            for (short PathCounter = 1, loopTo = Conversions.ToShort(MyPathNo); PathCounter <= loopTo; PathCounter++)
            {
                drPathOperations = mdsGraphicView.Tables["Tbl_OperationNetworkPaths"].Select("PathCode=" + Myds1.Tables["Tbl_OperationPaths"].Rows[PathCounter - 1]["PathCode"].ToString());
                // حلقۀ شمارندۀ عملیاتهای مسیر جاری
                for (short PathOperationCounter = 0, loopTo1 = (short)(drPathOperations.Length - 1); PathOperationCounter <= loopTo1; PathOperationCounter++)
                {
                    OperationCode = Conversions.ToString(drPathOperations[PathOperationCounter]["OperationCode"]);
                    // چک کردن اینکه آیا عملیات جاری قبلا رسم شده است یا نه
                    ExistOpIndex = CheckExist(OperationCode);
                    if (ExistOpIndex == -1)
                    {
                        ptOpDrawPoint = new Point(PathCounter * PATHS_BETWEEN_SPACE, (PathOperationCounter + 1) * OBJECTS_BETWEEN_SPACE);
                        ptOpDrawPoint.Y = ptOpDrawPoint.Y + OBJECTS_FIRSTSTEP_SPACE;

                        // چک کردن اینکه عملیات جاری دارای مواد وارده می باشد یا نه
                        drMaterials = mdsGraphicView.Tables["Tbl_OperationMaterials"].Select("CurrentOperationCode='" + OperationCode + "'");
                        if (drMaterials.Length > 0)
                        {
                            // چک کردن اینکه آیا عملیات اول می باشد یا نه
                            if (PathOperationCounter == 0)
                            {
                                for (short OperationMaterialCounter = 0, loopTo2 = (short)(drMaterials.Length - 1); OperationMaterialCounter <= loopTo2; OperationMaterialCounter++)
                                {
                                    ptMaDrawPoint = new Point(PathCounter * PATHS_BETWEEN_SPACE + OperationMaterialCounter * (GRIDSIZE + OBJECTS_MATERIALS_BETWEENSPACE), OBJECTS_FIRSTMATERIALS_STARTSPACE);
                                    AddSymbol(Conversions.ToString(drMaterials[OperationMaterialCounter]["MaterialCode"]), ptMaDrawPoint.X, ptMaDrawPoint.Y, (Bitmap)Image.FromFile(@"Images\slctctrl.BMP"), Conversions.ToString(drMaterials[OperationMaterialCounter]["MaterialCode"]));
                                    AddArrow(Conversions.ToString(drMaterials[OperationMaterialCounter]["MaterialCode"]), OperationCode, ptMaDrawPoint, ptOpDrawPoint);
                                }
                            }
                            else // عملیات اول نمی باشد
                            {
                                for (short OperationMaterialCounter = 0, loopTo3 = (short)(drMaterials.Length - 1); OperationMaterialCounter <= loopTo3; OperationMaterialCounter++)
                                {
                                    ptMaDrawPoint = new Point(OBJECTS_MIDLLEMATERIALS_STARTSPACE + PathCounter * PATHS_BETWEEN_SPACE + OperationMaterialCounter * (GRIDSIZE + OBJECTS_MATERIALS_BETWEENSPACE), (int)Math.Round(PathOperationCounter * OBJECTS_BETWEEN_SPACE + OBJECTS_BETWEEN_SPACE / 2d + OBJECTS_FIRSTSTEP_SPACE));
                                    AddSymbol(Conversions.ToString(drMaterials[OperationMaterialCounter]["MaterialCode"]), ptMaDrawPoint.X, ptMaDrawPoint.Y, (Bitmap)Image.FromFile(@"Images\slctctrl.BMP"), Conversions.ToString(drMaterials[OperationMaterialCounter]["MaterialCode"]));
                                    AddArrow(Conversions.ToString(drMaterials[OperationMaterialCounter]["MaterialCode"]), OperationCode, ptMaDrawPoint, ptOpDrawPoint);
                                }
                            }
                        }

                        // چک کردن اینکه عملیات جاری دارای عملیات پیشنیاز می باشد یا نه
                        drPreOperations = mdsGraphicView.Tables["Tbl_PreOperations"].Select("CurrentOperationCode='" + OperationCode + "'");
                        if (drPreOperations.Length > 0)
                        {
                            for (short PreOperationCounter = 0, loopTo4 = (short)(drPreOperations.Length - 1); PreOperationCounter <= loopTo4; PreOperationCounter++)
                            {
                                ExistOpIndex = CheckExist(Conversions.ToString(drPreOperations[PreOperationCounter]["PreOperationCode"]));
                                if (ExistOpIndex > -1)
                                {
                                    AddArrow(GetSymbol(ExistOpIndex).StepID, OperationCode, new Point(GetSymbol(ExistOpIndex).LocationRectangle.X, GetSymbol(ExistOpIndex).LocationRectangle.Y), ptOpDrawPoint);
                                }
                            }
                        }

                        // چک کردن اینکه عملیات جاری دارای عملیات پسنیاز می باشد یا نه
                        drAfterwardOperations = mdsGraphicView.Tables["Tbl_PreOperations"].Select("PreOperationCode='" + OperationCode + "'");
                        if (drAfterwardOperations.Length > 0)
                        {
                            for (short AfterwardOperationCounter = 0, loopTo5 = (short)(drAfterwardOperations.Length - 1); AfterwardOperationCounter <= loopTo5; AfterwardOperationCounter++)
                            {
                                ExistOpIndex = CheckExist(Conversions.ToString(drAfterwardOperations[AfterwardOperationCounter]["CurrentOperationCode"]));
                                if (ExistOpIndex > -1)
                                {
                                    AddArrow(OperationCode, GetSymbol(ExistOpIndex).StepID, ptOpDrawPoint, new Point(GetSymbol(ExistOpIndex).LocationRectangle.X, GetSymbol(ExistOpIndex).LocationRectangle.Y));
                                }
                            }
                        }

                        AddSymbol(OperationCode, ptOpDrawPoint.X, ptOpDrawPoint.Y, (Bitmap)Image.FromFile(GetImageName(OperationCode)), OperationCode);
                    }
                }

                ProgressBar1.Value = (int)Math.Round(PathCounter / (double)PathQuantity * 100d);
            }

            pbDiagram.Cursor = Cursors.Default;
            pbDiagram.Invalidate();
            ProgressBar1.Visible = false;
        }

        private clsSymbol GetSymbol(int I)
        {
            return (clsSymbol)DiagramPieces[I];
        }

        private clsArrow GetArrow(int I)
        {
            return (clsArrow)DiagramArrows[I];
        }

        private int CheckBounds(Point pt)
        {
            Rectangle rec;
            for (short I = 0, loopTo = (short)(DiagramPieces.Count - 1); I <= loopTo; I++)
            {
                rec = GetSymbol(I).LocationRectangle;
                if (rec.Contains(pt))
                {
                    return I;
                }
            }

            return -1;
        }

        private int CheckExist(string SymbolName)
        {
            for (short I = 0, loopTo = (short)(DiagramPieces.Count - 1); I <= loopTo; I++)
            {
                if ((GetSymbol(I).StepID ?? "") == (SymbolName ?? ""))
                {
                    return I;
                }
            }

            return -1;
        }

        private void AddSymbol(string SymbolName, int XLoc, int YLoc, Bitmap Icon, string Caption)
        {
            DiagramPieces.Add(new clsSymbol(SymbolName, XLoc, YLoc, Icon, Caption));
        }

        private void AddArrow(string StartSymbolName, string EndSymbolName, Point StartPoint, Point EndPoint)
        {
            DiagramArrows.Add(new clsArrow(StartSymbolName, EndSymbolName, StartPoint, EndPoint));
        }

        private string GetImageName(string OperationCode)
        {
            string ImageName = Constants.vbNullString;
            var drOpType = mdsGraphicView.Tables["Tbl_ProductOPCs"].Select("OperationCode='" + OperationCode + "'");
            if (drOpType.Length > 0)
            {
                var method = (EnumExecutionMethod)short.Parse(drOpType[0]["ExecutionMethod"].ToString());
                switch (method)
                {
                    case EnumExecutionMethod.EM_MACHINE:
                        {
                            ImageName = @"Images\slctaspg.BMP";
                            break;
                        }

                    case EnumExecutionMethod.EM_NONMACHINE:
                        {
                            ImageName = @"Images\slctcwkr.BMP";
                            break;
                        }

                    case EnumExecutionMethod.EM_CONTRACTOR:
                        {
                            ImageName = @"Images\slctwkr.BMP";
                            break;
                        }
                }
            }
            else
            {
                ImageName = "1.ico";
            }

            drOpType = null;
            return ImageName;
        }

        private class clsArrow
        {
            private Point mStartPoint = new Point();
            private Point mEndPoint = new Point();
            private string mStartID;
            private string mEndID;

            public clsArrow(string mSID, string mEID, Point mStart, Point mEnd)
            {
                mStartID = mSID;
                mEndID = mEID;
                mStartPoint = mStart;
                mEndPoint = mEnd;
            }

            public Point StartPoint
            {
                get
                {
                    return mStartPoint;
                }
            }

            public Point EndPoint
            {
                get
                {
                    return mEndPoint;
                }
            }

            public string StartID
            {
                get
                {
                    return mStartID;
                }
            }

            public string EndID
            {
                get
                {
                    return mEndID;
                }
            }

            public void DrawArrow(Graphics GraphicsObject)
            {
                var Pen = new Pen(Color.Black, 1f);
                Point pt1, pt2, pt3, pt4;
                if (mStartPoint.X == mEndPoint.X)
                {
                    pt1 = new Point((int)Math.Round(mStartPoint.X + GRIDSIZE / 2d), mStartPoint.Y + GRIDSIZE + 5);
                    pt2 = new Point(pt1.X, mEndPoint.Y - 5);
                    GraphicsObject.DrawLine(Pen, pt1, pt2);
                }
                else // If mStartPoint.X > mEndPoint.X Then
                {
                    pt1 = new Point((int)Math.Round(mStartPoint.X + GRIDSIZE / 2d), mEndPoint.Y - 10);
                    pt2 = new Point((int)Math.Round(mEndPoint.X + GRIDSIZE / 2d), pt1.Y);
                    pt3 = new Point(pt1.X, mStartPoint.Y + GRIDSIZE + 5);
                    pt4 = new Point(pt2.X, mEndPoint.Y - 5);
                    GraphicsObject.DrawLine(Pen, pt3, pt1);
                    GraphicsObject.DrawLine(Pen, pt1, pt2);
                    GraphicsObject.DrawLine(Pen, pt2, pt4);
                    // ElseIf mStartPoint.X < mEndPoint.X Then
                    // pt1 = New Point(mStartPoint.X + (GRIDSIZE / 2), mEndPoint.Y - 10)
                    // pt2 = New Point(mEndPoint.X + (GRIDSIZE / 2), pt1.Y)

                    // pt3 = New Point(pt1.X, mStartPoint.Y + GRIDSIZE + 5)
                    // pt4 = New Point(pt2.X, mEndPoint.Y - 5)

                    // GraphicsObject.DrawLine(Pen, pt3, pt1)
                    // GraphicsObject.DrawLine(Pen, pt1, pt2)
                    // GraphicsObject.DrawLine(Pen, pt2, pt4)
                }

                pt1 = default;
                pt2 = default;
                pt3 = default;
                pt4 = default;
                Pen = null;
            }

            public void SetStartPoint(Point pt)
            {
                mStartPoint = pt;
            }

            public void SetEndPoint(Point pt)
            {
                mEndPoint = pt;
            }
        }

        private class clsSymbol
        {
            private Bitmap mSymbolImage;
            private Rectangle mLocationRectangle = new Rectangle(0, 0, GRIDSIZE, GRIDSIZE);
            private string mStepID;
            private string mCaption;

            public clsSymbol(string ID, int xLocation, int yLocation, Bitmap SourceImage, string Caption)
            {
                mLocationRectangle.X = xLocation;
                mLocationRectangle.Y = yLocation;
                mStepID = ID;
                mCaption = Caption;
                mSymbolImage = SourceImage.Clone(new Rectangle(0, 0, GRIDSIZE, GRIDSIZE), System.Drawing.Imaging.PixelFormat.DontCare);
            }

            public Rectangle LocationRectangle
            {
                get
                {
                    return mLocationRectangle;
                }
            }

            public string StepID
            {
                get
                {
                    return mStepID;
                }
            }

            public void Draw(Graphics GraphicsObject)
            {
                GraphicsObject.DrawImage(mSymbolImage, mLocationRectangle);
                if (!string.IsNullOrEmpty(mCaption))
                {
                    float X, Y;
                    string tmpCaption = mCaption; // Mid(mCaption, 1, InStr(mCaption, "/") - 1)
                    int W = 70;
                    var fnt = new Font("Tahoma", 8f, FontStyle.Regular);
                    X = mLocationRectangle.X - 75;
                    Y = mLocationRectangle.Y + 5;
                    var TextSize = new RectangleF(X, Y, W, 20f);
                    GraphicsObject.DrawString(tmpCaption, fnt, Brushes.Blue, TextSize);

                    // tmpCaption = mCaption 'Mid(mCaption, InStr(mCaption, "/") + 1, Len(mCaption))

                    // Y = Y + 22

                    // TextSize = New RectangleF(X, Y, W, 20)

                    // GraphicsObject.DrawString(tmpCaption, fnt, Brushes.Blue, TextSize)
                }
            }

            public void SetLocation(int xLocation, int yLocation)
            {
                mLocationRectangle.X = xLocation;
                mLocationRectangle.Y = yLocation;
            }
        }
    }
}
'Mouse_Move_Prediction_VB
'frmMain.vb
'
'Emgu CV 3.1.0
'
'form components:
'tableLayoutPanel
'imageBox
'txtInfo

Option Explicit On      'require explicit declaration of variables, this is NOT Python !!
Option Strict On        'restrict implicit data type conversions to only widening conversions

Imports Emgu.CV                 '
Imports Emgu.CV.CvEnum          'usual Emgu Cv imports
Imports Emgu.CV.Structure       '
Imports Emgu.CV.UI              '
Imports Emgu.CV.Util            '

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Class frmMain

    ' member variables ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Dim SCALAR_BLACK As New MCvScalar(0.0, 0.0, 0.0)
    Dim SCALAR_WHITE As New MCvScalar(255.0, 255.0, 255.0)
    Dim SCALAR_BLUE As New MCvScalar(255.0, 0.0, 0.0)
    Dim SCALAR_GREEN As New MCvScalar(0.0, 255.0, 0.0)
    Dim SCALAR_RED As New MCvScalar(0.0, 0.0, 255.0)

    Dim currentMousePosition As New Point()

    Dim mousePositions As New List(Of Point)

    Dim predictedMousePosition As New List(Of Point)

    Dim imgBlank As Mat

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub imageBox_MouseMove(sender As Object, e As MouseEventArgs) Handles imageBox.MouseMove
        
        currentMousePosition = imageBox.PointToClient(Cursor.Position)
        
        mousePositions.Add(currentMousePosition)

        Dim predictedMousePosition As Point = predictNextPosition(mousePositions)

        txtInfo.AppendText("current position        = " + mousePositions.Last().X.ToString() + ", " + mousePositions.Last().Y.ToString() + vbCrLf)
        txtInfo.AppendText("next predicted position = " + predictedMousePosition.X.ToString() + ", " + predictedMousePosition.Y.ToString() + vbCrLf)
        txtInfo.AppendText("--------------------------------------------------" + vbCrLf)

        imgBlank = New Mat(imageBox.Size(), DepthType.Cv8U, 3)

        DrawCross(imgBlank, mousePositions.Last(), SCALAR_WHITE)
        DrawCross(imgBlank, predictedMousePosition, SCALAR_BLUE)

        imageBox.Image = imgBlank

        Application.DoEvents()
        
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Function predictNextPosition(ByRef positions As List(Of Point)) As Point

        Dim predictedPosition As New Point()            'this will be the return value
        
        Dim numPositions As Integer = mousePositions.Count()

        If (numPositions = 1) Then
            
            Return(positions(0))

        ElseIf (numPositions = 2) Then

            Dim deltaX As Integer = positions(1).X - positions(0).X
            Dim deltaY As Integer = positions(1).Y - positions(0).Y

            predictedPosition.X = positions.Last().X + deltaX
            predictedPosition.Y = positions.Last().Y + deltaY

        ElseIf (numPositions = 3) Then

            Dim sumOfXChanges As Integer = ((positions(2).X - positions(1).X) * 2) +
                                           ((positions(1).X - positions(0).X) * 1)

            Dim deltaX As Integer = CInt(Math.Round(CDbl(sumOfXChanges / 3.0)))

            Dim sumOfYChanges As Integer = ((positions(2).Y - positions(1).Y) * 2) +
                                           ((positions(1).Y - positions(0).Y) * 1)

            Dim deltaY As Integer = CInt(Math.Round(CDbl(sumOfYChanges / 3.0)))

            predictedPosition.X = positions.Last().X + deltaX
            predictedPosition.Y = positions.Last().Y + deltaY
            
        ElseIf (numPositions = 4) Then

            Dim sumOfXChanges As Integer = ((positions(3).X - positions(2).X) * 3) +
                                           ((positions(2).X - positions(1).X) * 2) +
                                           ((positions(1).X - positions(0).X) * 1)

            Dim deltaX As Integer = CInt(Math.Round(CDbl(sumOfXChanges / 6.0)))

            Dim sumOfYChanges As Integer = ((positions(3).Y - positions(2).Y) * 3) +
                                           ((positions(2).Y - positions(1).Y) * 2) +
                                           ((positions(1).Y - positions(0).Y) * 1)

            Dim deltaY As Integer = CInt(Math.Round(CDbl(sumOfYChanges / 6.0)))

            predictedPosition.X = positions.Last().X + deltaX
            predictedPosition.Y = positions.Last().Y + deltaY

        ElseIf (numPositions >= 5) Then

            Dim sumOfXChanges As Integer = ((positions(numPositions - 1).X - positions(numPositions - 2).X) * 4) +
                                           ((positions(numPositions - 2).X - positions(numPositions - 3).X) * 3) +
                                           ((positions(numPositions - 3).X - positions(numPositions - 4).X) * 2) +
                                           ((positions(numPositions - 4).X - positions(numPositions - 5).X) * 1)

            Dim deltaX As Integer = CInt(Math.Round(CDbl(sumOfXChanges / 10.0)))

            Dim sumOfYChanges As Integer = ((positions(numPositions - 1).Y - positions(numPositions - 2).Y) * 4) +
                                           ((positions(numPositions - 2).Y - positions(numPositions - 3).Y) * 3) +
                                           ((positions(numPositions - 3).Y - positions(numPositions - 4).Y) * 2) +
                                           ((positions(numPositions - 4).Y - positions(numPositions - 5).Y) * 1)

            Dim deltaY As Integer = CInt(Math.Round(CDbl(sumOfYChanges / 10.0)))

            predictedPosition.X = positions.Last().X + deltaX
            predictedPosition.Y = positions.Last().Y + deltaY
            
        Else

            'should never get here

        End If
        
        Return(predictedPosition)

    End Function

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Sub DrawCross(ByVal imgBlank As Mat, center As Point, color As MCvScalar)
        CvInvoke.Line(imgBlank, New Point(center.X - 5, center.Y - 5), New Point(center.X + 5, center.Y + 5), color, 2)
        CvInvoke.Line(imgBlank, New Point(center.X + 5, center.Y - 5), New Point(center.X - 5, center.Y + 5), color, 2)
    End Sub

End Class



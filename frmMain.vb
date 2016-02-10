'Mouse_Move_Prediction_VB
'frmMain.vb
'
'Emgu CV 3.0.0
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

    Dim predictedMousePositions As New List(Of Point)

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub imageBox_MouseMove(sender As Object, e As MouseEventArgs) Handles imageBox.MouseMove

        currentMousePosition = imageBox.MousePosition()

        Dim predictedMousePosition As Point = predictNextPosition(mousePositions)





    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Function predictNextPosition(ByRef mousePositions As List(Of Point)) As Point

        Dim predictedPosition As New Point()








        Return predictedPosition

    End Function

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Sub DrawCross()



    End Sub

End Class

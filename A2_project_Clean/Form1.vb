Public Class Form1
    Dim Floor As CFloor
    Dim Body As CBody

    Public NumOfLegs As Integer = 3
    Dim NumOfLayers As Integer = 2
    Dim Line(NumOfLegs - 1, NumOfLayers - 1) As CLeg
    Dim Joint(NumOfLegs - 1, NumOfLayers - 1) As CJoint

    Dim Rnd As New Random


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'create floor object
        floor = New CFloor(500)
        Body = New CBody(100, 0, 300, 0)

        For y = 0 To NumOfLayers - 1
            For x = 0 To NumOfLegs - 1
                Line(x, y) = New CLeg(50 + 100 * x, 50 + 100 * y, Rnd)
                joint(x, y) = New CJoint(50 + 100 * x, 50 + 100 * y, 10, 10, 1)
            Next
        Next

        For x = 0 To NumOfLegs - 1
            Body.BodyPoints(Line(x, 0).lp1, NumOfLegs)
        Next
    End Sub
End Class

Public Class CBody
    Public Connections(Form1.NumOfLegs) As PointF
    Public StaticConnections(Form1.NumOfLegs) As PointF
    Public CoM As PointF

    Public Bypos1 As Double
    Public Bypos2 As Double
    Public Bxpos1 As Double
    Public Bxpos2 As Double

    Public Yspeed As Integer = 0

    Public BPx1 As Double
    Public BPx2 As Double
    Public BPy1 As Double
    Public BPy2 As Double


    Public Sub New(x1, y1, x2, y2)
        Bypos1 = y1
        Bxpos1 = x1
        Bypos2 = y2
        Bxpos2 = x2
        CoM = New Point((x1 + x2) / 2, (y1 + y2) / 2)
        BPx1 = x1
        Bpx2 = x2
        Bpy1 = y1
        Bpy2 = y2
    End Sub

    Sub BodyPoints(P1Legs As PointF, NumOfLegs As Integer)
        Connections(NumOfLegs - 1) = P1Legs
        StaticConnections(NumOfLegs - 1) = P1Legs
    End Sub
End Class

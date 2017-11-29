﻿Public Class CBody
    Public Connections(Form1.NumOfLegs - 1) As PointF
    Public StaticConnections(Form1.NumOfLegs - 1) As PointF
    Public CoM As PointF

    Public Bypos1 As Double
    Public Bypos2 As Double
    Public Bxpos1 As Double
    Public Bxpos2 As Double

    Public Yspeed As Double = 0

    Public BPx1 As Double
    Public BPx2 As Double
    Public BPy1 As Double
    Public BPy2 As Double

    Public BPx11 As Double
    Public BPx22 As Double
    Public BPy11 As Double
    Public BPy22 As Double


    Public BPx111 As Double
    Public BPx222 As Double
    Public BPy111 As Double
    Public BPy222 As Double

    Public AngleIncrease As Double = 0
    Public increasetest As Double = 0

    Public BodyRise As Double = 0

    Public LeftMomentum As Double = 0.1
    Public RightMomentum As Double = 0.1

    Public Pivot As PointF
    Public PrevPivot As PointF
    Public Leftside As Boolean
    Public Rightside As Boolean

    Public Friction As Double
    Public NegativeFriction As Double
    Public PrevCoM As New PointF(0, 0)

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

    Sub BodyPoints(P1Legs As PointF, x As Integer)
        Connections(x) = P1Legs
        StaticConnections(x) = P1Legs
    End Sub

    Public Sub SetPoints(line As CLeg, x As Integer)
        'set points to body 
        line.LPx2 += Connections(x).X - line.LPx1
        line.LPy2 += Connections(x).Y - line.LPy1
        line.LP1 = Connections(x)
        line.LYpos = line.LP1.Y
        line.LPy1 = line.LP1.Y
        line.LPx1 = line.LP1.X
        line.LXpos = line.LP1.X
    End Sub

    Sub RaiseBody(Floor As CFloor)
        If Floor.ypos <= BodyRise Then
            BPy1 -= (BodyRise - Floor.ypos)
            BPy2 -= (BodyRise - Floor.ypos)
            For x = 0 To 2
                Connections(x).Y -= (BodyRise - Floor.ypos)
                StaticConnections(x).Y -= (BodyRise - Floor.ypos)
            Next
            Bypos1 -= (BodyRise - Floor.ypos)
            Bypos2 -= (BodyRise - Floor.ypos)

            ResetCoM()


        End If
    End Sub

    Sub ResetCoM()
        CoM = New Point((BPx1 + BPx2) / 2, (BPy1 + BPy2) / 2)
    End Sub

    Sub FindPivot(Line As CLeg, floor As CFloor)
        If Leftside = True And Rightside = False And Form1.CheckFloor(floor, Line.Pivot.Y) = True Then

            If Pivot.X < Line.Pivot.X Or Form1.CheckFloor(floor, Pivot.Y) = False Then
                Pivot = Line.Pivot
            End If

        ElseIf Leftside = False And Rightside = True And Form1.checkfloor(floor, Line.Pivot.Y) = True Then

            If Pivot.X > Line.Pivot.X Or Form1.CheckFloor(floor, Pivot.Y) = False Then
                Pivot = Line.Pivot
            End If

        End If
    End Sub

    Sub FallRight(Pivot As PointF, g As Graphics)
        AngleIncrease = 1 * RightMomentum
        RightMomentum -= 0.2

        For x = 0 To 2
            Connections(x).X = ((StaticConnections(x).X - Pivot.X) * Math.Cos(AngleIncrease * Math.PI / 180)) + ((StaticConnections(x).Y - Pivot.Y) * -(Math.Sin(AngleIncrease * Math.PI / 180))) + Pivot.X
            Connections(x).Y = ((StaticConnections(x).X - Pivot.X) * Math.Sin(AngleIncrease * Math.PI / 180)) + ((StaticConnections(x).Y - Pivot.Y) * Math.Cos(AngleIncrease * Math.PI / 180)) + Pivot.Y
        Next
        BPx1 = ((Bxpos1 - Pivot.X) * Math.Cos(AngleIncrease * Math.PI / 180)) + ((Bypos1 - Pivot.Y) * -(Math.Sin(AngleIncrease * Math.PI / 180))) + Pivot.X
        BPy1 = ((Bxpos1 - Pivot.X) * Math.Sin(AngleIncrease * Math.PI / 180)) + ((Bypos1 - Pivot.Y) * Math.Cos(AngleIncrease * Math.PI / 180)) + Pivot.Y
        BPx2 = ((Bxpos2 - Pivot.X) * Math.Cos(AngleIncrease * Math.PI / 180)) + ((Bypos2 - Pivot.Y) * -(Math.Sin(AngleIncrease * Math.PI / 180))) + Pivot.X
        BPy2 = ((Bxpos2 - Pivot.X) * Math.Sin(AngleIncrease * Math.PI / 180)) + ((Bypos2 - Pivot.Y) * Math.Cos(AngleIncrease * Math.PI / 180)) + Pivot.Y


    End Sub

    Sub FallLeft(Pivot As PointF, G As Graphics)

        AngleIncrease = 1 * LeftMomentum
        LeftMomentum += 0.2

        For x = 0 To 2
            Connections(x).X = ((StaticConnections(x).X - Pivot.X) * Math.Cos(AngleIncrease * Math.PI / 180)) + ((StaticConnections(x).Y - Pivot.Y) * -(Math.Sin(AngleIncrease * Math.PI / 180))) + Pivot.X
            Connections(x).Y = ((StaticConnections(x).X - Pivot.X) * Math.Sin(AngleIncrease * Math.PI / 180)) + ((StaticConnections(x).Y - Pivot.Y) * Math.Cos(AngleIncrease * Math.PI / 180)) + Pivot.Y
        Next
        BPx1 = ((Bxpos1 - Pivot.X) * Math.Cos(AngleIncrease * Math.PI / 180)) + ((Bypos1 - Pivot.Y) * -(Math.Sin(AngleIncrease * Math.PI / 180))) + Pivot.X
        BPy1 = ((Bxpos1 - Pivot.X) * Math.Sin(AngleIncrease * Math.PI / 180)) + ((Bypos1 - Pivot.Y) * Math.Cos(AngleIncrease * Math.PI / 180)) + Pivot.Y
        BPx2 = ((Bxpos2 - Pivot.X) * Math.Cos(AngleIncrease * Math.PI / 180)) + ((Bypos2 - Pivot.Y) * -(Math.Sin(AngleIncrease * Math.PI / 180))) + Pivot.X
        BPy2 = ((Bxpos2 - Pivot.X) * Math.Sin(AngleIncrease * Math.PI / 180)) + ((Bypos2 - Pivot.Y) * Math.Cos(AngleIncrease * Math.PI / 180)) + Pivot.Y

    End Sub
    Sub FallOver(g As Graphics, line(,) As CLeg)

        'BPy111 = BPy11
        'BPx111 = BPx11
        'BPy222 = BPy22
        'BPx222 = BPx22

        'BPy11 = BPy1
        'BPx11 = BPx1
        'BPy22 = BPy2
        'BPx22 = BPx2

        AngleIncrease = 0
        Bxpos1 = BPx1
        Bxpos2 = BPx2
        Bypos1 = BPy1
        Bypos2 = BPy2
        For x = 0 To Form1.NumOfLegs - 1
            StaticConnections(x) = Connections(x)
        Next
        If Leftside = True And Rightside = False Then
            FallLeft(Pivot, g)
        ElseIf Leftside = False And Rightside = True Then
            FallRight(Pivot, g)
        End If
        PrevPivot = Pivot
        ResetCoM()
        For y = 0 To Form1.NumOfLayers - 1
            For x = 0 To Form1.NumOfLegs - 1
                line(x, y).FallOver(Pivot, AngleIncrease)
            Next
        Next

    End Sub

    Sub Drop()
        'add speed to fall
        Bypos1 += Yspeed
        Bypos2 += Yspeed

        For x = 0 To 2
            Connections(x).Y += Yspeed
            StaticConnections(x).Y += Yspeed
        Next
        BPy1 += Yspeed
        BPy2 += Yspeed
        Yspeed += 1

        ResetCoM()
    End Sub

    Sub FindFriction(Line As CLeg, Floor As CFloor)
        Friction = 0
        NegativeFriction = 0

        If Line.OldPoint1.X - Line.LPx1 > Friction And Form1.CheckFloor(Floor, Line.LPy1) = True And Form1.CheckFloor(Floor, Line.OldPoint1.Y) Then
            Friction = (Line.OldPoint1.X - Line.LPx1)
        End If

        If Line.OldPoint2.X - Line.LPx2 > Friction And Form1.CheckFloor(Floor, Line.LPy2) = True And Form1.CheckFloor(Floor, Line.OldPoint2.Y) Then
            Friction = (Line.OldPoint2.X - Line.LPx2)
        End If

        If Line.OldPoint1.X - Line.LPx1 < NegativeFriction And Form1.CheckFloor(Floor, Line.LPy1) = True And Form1.CheckFloor(Floor, Line.OldPoint1.Y) Then
            NegativeFriction = (Line.OldPoint1.X - Line.LPx1)
        End If

        If Line.OldPoint2.X - Line.LPx2 < NegativeFriction And Form1.CheckFloor(Floor, Line.LPy2) = True And Form1.CheckFloor(Floor, Line.OldPoint2.Y) Then
            NegativeFriction = (Line.OldPoint2.X - Line.LPx2)
        End If


    End Sub

    Sub AddFriction()

        BPx1 += Friction + NegativeFriction
        BPx2 += Friction + NegativeFriction
        For x = 0 To 2
            Connections(x).X += Friction + NegativeFriction
            StaticConnections(x).X += Friction + NegativeFriction
        Next
        Bxpos1 += Friction + NegativeFriction
        Bxpos2 += Friction + NegativeFriction

        ResetCoM()


    End Sub

    Sub Draw(g As Graphics)

        g.DrawLine(Pens.Black, Convert.ToInt32(BPx1), Convert.ToInt32(BPy1), Convert.ToInt32(BPx2), Convert.ToInt32(BPy2))

    End Sub
End Class

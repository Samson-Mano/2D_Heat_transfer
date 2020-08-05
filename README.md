# 2D Heat transfer solver 
Finite element analysis of steady state 2D heat transfer problems. Heat transfer occurs when there is a temperature difference within a body or within a body and its surrounding medium. Conduction and convection problems are solved using this software. <br /><br /> Heat diffusion equation which is the special case partial differential equation of the Helmholtz equation is solved.<br /><br /><br /><br />

## How to use this software:<br />
Go to Pre-processing -> Import mesh <br />
<br />
Mesh format is <br />
** <br />
** <br />
**   Template:  Heat 2D Program <br />
** <br />
*NODE <br />
	1,	1.0,	-1.0 <br />
	2,	1.0,	-0.5 <br />
	3,	1.0,	 0.0 <br />
	4,	1.0,	 0.5 <br />
	5,	1.0,	 1.0 <br />
	6,	0.0,	-1.0 <br />
	7,	0.0,	-0.5 <br />
	8,	0.0,	 0.0 <br />
	9,	0.0,	 0.5 <br />
	10,	0.0,	 1.0 <br />
*ELEMENT,TYPE=S3 <br />
         1,       1,       6,       2 <br />
         2,       6,       7,       2 <br />
         3,       2,       7,       3 <br />
         4,       7,       8,       3 <br />
         5,       3,       8,       4 <br />
         6,       8,       9,       4 <br />
         7,       4,       9,       5 <br />
         8,       9,      10,       5 <br />
<br />
Use Pre-processing menu to apply boundary conditions<br />
<br />
## Example 1:<br /><br />
Heat conduction problem with inside and outside convective boundary with 150 deg & 10 ambient temperature respectively. <br /><br />
![](Images/Example_1_problem.png)<br /><br />
![](Images/Example_1_mesh.png)<br /><br />
![](Images/Example_1_solved.png)<br /><br />
## Example 2:<br /><br />
Heat transfer problem with point heat source supplied by heating cables and convective boundary at -5 deg ambient temperature. Symmetry boundary condition is used to solve this problem<br /><br />
![](Images/Example_2_problem.png)<br /><br />
![](Images/Example_2_soln.png)<br /><br />
## Example 3:<br /><br />
Heat transfer problemn with prescribed inner temperature of 140 deg and outter convective boundary with ambient temperature 20 deg.<br /><br />
![](Images/Example_3_problem.png)<br /><br />
![](Images/Example_3_soln.png)<br /><br />
## Example 4:<br /><br />
A hot pipe running through the thin plate results in the inner surface maintained at 80 deg. The two dimensional fin is subjected to convection with ambient air temperature being 20 deg.
![](Images/Example_4_problem.png)<br /><br />
![](Images/Example_4_soln.png)<br /><br />
# Theory
Please refer to attachment Theory_behind_2dheat_program.pdf for reference<br /><br />
# Reference
1. Concepts and Application of finite element analysis (Fourth Edition) – Robert D.
Cook, David S. Malkus, Michael E. Plesha, Robert J. Witt<br /><br />
2. Introduction to Finite Elements in Engineering (Third Edition) – Tirupathi R.
Chandrupatla, Ashok D. Belegundu<br /><br />
3. A First Course in Finite Element Method – Daryl L. Logan<br /><br />
4. Applied Finite Element Analysis (Second Edition) – Larry. J. Segerlind<br /><br />
5. MATLAB Codes for finite element analysis – A. J. M. Ferreira<br /><br />
6. Finite Element Procedures (Second Edition) – Klaus-Jürgen Bathe<br /><br />
7. NPTEL :: Civil Engineering Finite Element Analysis<br /><br />

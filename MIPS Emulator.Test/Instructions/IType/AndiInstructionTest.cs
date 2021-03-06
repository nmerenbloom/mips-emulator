﻿using MIPS_Emulator.Instructions.IType;
using NUnit.Framework;

namespace MIPS_Emulator.Test.Instructions.IType {
	public class AndiInstructionTest {
		private uint pc;
		private Registers reg;
		private MemoryMapper mem;
		private AndiInstruction target;

		[SetUp]
		public void SetUp() {
			pc = 0x00000000;
			reg = new Registers();
			mem = new MemoryMapper(8);
		}

		[Test]
		public void Execute() {
			reg[8] = 0x00FFFF00;
			target = new AndiInstruction(9, 8, 0xFFFF);
			
			target.Execute(ref pc, mem, reg);
			
			Assert.AreEqual(0x0000FF00, reg[9]);
			Assert.AreEqual(0x00000004, pc);
		}

		[Test]
		public void ToString_Formatted() {
			target = new AndiInstruction(9, 8, 0xFFFF);
			
			Assert.AreEqual("ANDI $t1, $t0, 0xFFFF", target.ToString());
		}
	}
}
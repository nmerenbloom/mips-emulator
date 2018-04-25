﻿using System;

namespace MIPS_Emulator.Instructions.RType {
	public class AddInstruction : RTypeInstruction {
		protected override string Name => "ADD";
		
		public AddInstruction(uint d, uint s, uint t) : base(d, s, t) {
			
		}

		public override void Execute(ref uint pc, MemoryMapper mem, Registers reg) {
			reg[D] = reg[S] + reg[T];
			pc += 4;
		}
	}
}
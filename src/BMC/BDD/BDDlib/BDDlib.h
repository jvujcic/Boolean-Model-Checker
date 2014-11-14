// BDDlib.h

#pragma once
#pragma managed

#include "bdduser.h"
#include "Bdd.h"


namespace BDDlib {

	public ref class BddManager
	{
	private: 
		bdd_manager m_Manager;

	public:
		//Put in a try-catch block before doing anything with BDD or BDDManager
		void CheckBddInManager(Bdd^ f);
		void CheckUnmanedPointer();

		// Constructor
		BddManager();

		// Destructor
		~BddManager();

		// Finalizer
		!BddManager();

		// Returns statistics for BDD Manager
		System::String^ Statistics();

		// Creates a new variable at the START(END) of BDD variable ordering and returns a BDD for it
		Bdd^ CreateBddVariableFirst();
		Bdd^ CreateBddVariableLast();
		
		// Creates a new variable which is BEFORE(AFTER) given variable in the BDD variable ordering and returns a BDD for it
		Bdd^ CreateBddVariableBefore(Bdd^ bddVariable);
		Bdd^ CreateBddVariableAfter(Bdd^ bddVariable);
		
		// Creates BDD for constant TRUE(FALSE)
		Bdd^ CreateBddOne();
		Bdd^ CreateBddZero();

		// Returns the BDD for logical AND, OR, NOR, XOR, ...
		Bdd^ LogicalAnd(Bdd^ f, Bdd^ g); 
		Bdd^ LogicalOr(Bdd^ f, Bdd^ g);
		Bdd^ LogicalXor(Bdd^ f, Bdd^ g);
		Bdd^ LogicalNor(Bdd^ f, Bdd^ g);
		Bdd^ LogicalNand(Bdd^ f, Bdd^ g);
		Bdd^ LogicalNot(Bdd^ f);
		Bdd^ LogicalXnor(Bdd^ f, Bdd^ g);
		Bdd^ LogicalImplies(Bdd^ g, Bdd^ f);

		// Returns the BDD for logical IF f THEN g ELSE h
		Bdd^ LogicalIfThenElse(Bdd^ f, Bdd^ g, Bdd^ h);

		// Returns the BDD for the substitution of h for the variable g in f. When h does not depend on g,
		// the operation may be viewed as composition of boolean functions. If h does depend on g, it corresponds
		// to instantaneous substitution in a boolean formula.
		Bdd^ Compose(Bdd^ f, Bdd^ g, Bdd^ h);

		// Returns BDD variable with given index. If it doesn't exist returns NULL.
		Bdd^ GetBddVariableWithIndex(long index);

		// Returns BDD variable with given ID. If it doesn't exist returns NULL.
		Bdd^ GetBddVariableWithID(long id);

		// Forces a BDD garbage collection
		void ForceGarbageCollection();	

		// Deletes all nodes and variables in BDD manager
		void DeleteAll();
	};
}
